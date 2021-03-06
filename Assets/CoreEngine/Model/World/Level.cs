﻿using System;
using System.Collections.Generic;

using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.saving;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.wall;
using com.gStudios.isometric.model.world.furniture;
using com.gStudios.isometric.model.world.generation;

namespace com.gStudios.isometric.model.world {

	public class Level : ITileObserver, IWallObserver, IFurnitureObserver {

		ITile[,] tiles;
		IWall[,,] walls;
        List<ICharacter> characters;

		int width;

		public int Width {
			get {
				return width;
			}
		}

		int height;

		public int Height {
			get {
				return height;
			}
		}

        private List<ITileObserver> tileObservers;
        private List<IWallObserver> wallObservers;
        private List<IFurnitureObserver> furnitureObservers;

		public Level(int width, int height) {
			this.width = width;
			this.height = height;

            tileObservers = new List<ITileObserver>();
            wallObservers = new List<IWallObserver>();
            furnitureObservers = new List<IFurnitureObserver>();

            tiles = TileGenerator.Generate(this, width, height);
			walls = WallGenerator.Generate(this, width, height);
            characters = new List<ICharacter>();            
		}

        // Tiles
		public bool IsTileInBounds(int x, int y) {
			return (x < width && x >= 0 && y < height && y >= 0);
		}

		public ITile GetTileAt(int x, int y) {
			if (!IsTileInBounds(x, y)) {
				return new NullTile(x, y);
			}

			return tiles [x, y];
		}

        void ITileObserver.NotifyTileTypeChanged(ITile tile) {
            foreach (ITileObserver observer in tileObservers) {
                observer.NotifyTileTypeChanged(tile);
            }
        }

        public void SubscribeToTileChanges(ITileObserver observer) {
            if (tileObservers.Contains(observer))
                UnityEngine.Debug.LogError("Trying to add an observer more than once.");

            tileObservers.Add(observer);
        }

        // Walls
		public bool IsWallInBounds(int x, int y, int z) {
            if (z != 0 && z != 1)
                return false;

            if (x < 0 || y < 0)
                return false;

            if (x > width || y > height)
                return false;
            return true;
		}

        public IWall GetWallAt(int x, int y, int z) {
            if (!IsWallInBounds(x, y, z)) {
                UnityEngine.Debug.LogError("Wall (" + x + "," + y + "," + z + ") is out of range.");
                return null;
            }
            if (z != 0 && z != 1) {
                UnityEngine.Debug.LogError("Wall (" + x + "," + y + "," + z + ") is out of range.");
                return null;
            }

            return walls[x, y, z];
        }

        public IWall GetWallBetweenTiles(int x0, int y0, int x1, int y1) {
            float distance = Math.Abs(x0 - x1) + Math.Abs(y0 - y1);
            if (distance != 1) {
                UnityEngine.Debug.LogError("Tiles must be at a distance of exactly one.");
                return null;
            }

            int x = Math.Max(x0, x1);
            int y = Math.Max(y0, y1);
            int z = x0 == x1 ? 0 : 1;

            return GetWallAt(x, y, z);
        }

        void IWallObserver.NotifyWallTypeChanged(IWall wall) {
            foreach (IWallObserver observer in wallObservers) {
                observer.NotifyWallTypeChanged(wall);
            }
        }

        public void SubscribeToWallChanges(IWallObserver observer) {
            if (wallObservers.Contains(observer))
                UnityEngine.Debug.LogError("Trying to add an observer more than once.");

            wallObservers.Add(observer);
        }

        // Furniture
        void IFurnitureObserver.NotifyFurnitureTypeChanged(ITile tile) {
            foreach (IFurnitureObserver observer in furnitureObservers) {
                observer.NotifyFurnitureTypeChanged(tile);
            }
        }

        public void SubscribeToFurnitureChanges(IFurnitureObserver observer) {
            if (furnitureObservers.Contains(observer)) {
                UnityEngine.Debug.LogError("Trying to add an observer more than once.");
            }

            furnitureObservers.Add(observer);
        }

        // Vertex
        public bool IsVertexInBounds(int x, int y) {
            return IsWallInBounds(x, y, 0);
        }		

        // Characters
        public void AddCharacter(ICharacter character) {
            characters.Add(character);
        }

        public void RemoveCharacter(ICharacter character) {
            if (characters.Contains(character) == false)
                UnityEngine.Debug.LogError("Trying to remove a character that's not on the level.");

            characters.Remove(character);
        }

        public List<ICharacter> GetCharacters() {
            return characters;
        }

        // Serialization
        public void Save(ILevelSerializer levelSerializer) {
            levelSerializer.SaveLevel(this, tiles, walls);
        }

        // Randomization
		public void RandomizeTiles() {
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (UnityEngine.Random.Range(0,2) == 0) {
						tiles [x, y].Type = 0;
					}
					else {
						tiles [x, y].Type = 1;
					}
				}
			}
		}

		public void RandomizeWalls() {
			for (int x = 0; x < width + 1; x++) {
				for (int y = 0; y < height + 1; y++) {

					walls [x, y, 0].Type = UnityEngine.Random.Range (0, 2) == 0 ? 0 : 1;
					walls [x, y, 1].Type = UnityEngine.Random.Range (0, 2) == 0 ? 0 : 1;

				}
			}
		}

    }

}