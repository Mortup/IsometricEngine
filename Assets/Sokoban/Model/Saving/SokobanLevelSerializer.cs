using System.Text.RegularExpressions;

using UnityEngine;

using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.saving;
using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.wall;

using com.gStudios.sokoban.model.world;
using System.Collections.Generic;

namespace com.gStudios.sokoban.model.saving {

	public class SokobanLevelSerializer : ILevelSerializer{

        const char WALL = '#';
        const char PLAYER = '@';
        const char PLAYER_OVER_GOAL = '+';
        const char BOX = '$';
        const char PLACED_BOX = '*';
        const char GOAL = '.';
        const char FLOOR = ' ';

        private readonly string levelData = "";

        public SokobanLevelSerializer(string levelName) {
            TextAsset levelTxt = Resources.Load("Levels/" + levelName) as TextAsset;

            if (levelTxt == null)
                Debug.LogError("Could not load sokoban levels.");

            levelData = levelTxt.ToString();
        }

        public static int LevelsCount() {
            return Resources.LoadAll<TextAsset>("Levels").Length;
        }

        public bool ExistsSavedLevel() {
            return levelData != "";
        }

        public Level LoadLevel() {
            // Load level data
            string[] lines = Regex.Split(levelData, "\r\n|\r|\n");

            // Define width and height
            int height = lines.Length - 1;
            int width = lines[0].Length;

            // Lines are not the same width.
            // We should use the max width.
            for (int i = 1; i < lines.Length; i++) {
                if (lines[i].Length > width)
                    width = lines[i].Length;
            }

            // Create the array that will hold the data
            char[,] levelTiles = GetLevelTiles(lines, width, height);
            int[,] mask = FloodFill(levelTiles, FindPlayerPos(levelTiles));

            // The result.
            Level level = new Level(width, height);

            // Add tiles and items to the level.
            for (int x = 0; x < level.Width; x++) {
                for (int y = 0; y < level.Height; y++) {

                    char currentChar = levelTiles[x, y];
                    ITile currentTile = level.GetTileAt(x, y);

                    if (mask[x,y] == 1) {
                        currentTile.Type = GetTileFromChar(currentChar);
                    }
                    else {
                        currentTile.Type = 0;
                    }                    

                    if (currentChar == BOX || currentChar == PLACED_BOX) {
                        currentTile.PlaceFurniture(new SokobanBox(level, currentTile));
                    }
                    else if (currentChar == WALL) {
                        currentTile.PlaceFurniture(new SokobanWall(level, currentTile));
                    }
                    else if (currentChar == PLAYER || currentChar == PLAYER_OVER_GOAL) {
                        level.AddCharacter(new Character(level, x, y));
                    }

                }
            }

            // Delete walls.
            for (int x = 0; x < level.Width + 1; x++) {
                for (int y = 0; y < level.Height + 1; y++) {
                    for (int z = 0; z < 2; z++) {
                        level.GetWallAt(x, y, z).Type = 0;
                    }
                }
            }

            return level;
        }

        public void SaveLevel(Level level, ITile[,] tiles, IWall[,,] walls) {
            Debug.LogError("Sokoban Serializer does not suppor saving levels.");
            throw new System.NotImplementedException();
        }

        private char[,] GetLevelTiles(string[] lines, int width, int height) {
            char[,] tiles = new char[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    if (x < lines[y].Length) {
                        tiles[x, y] = lines[y][x];
                    }
                    else {
                        tiles[x, y] = FLOOR;
                    }
                }
            }

            return tiles;
        }

        private Vector2Int FindPlayerPos(char[,] tiles) {
            for (int x = 0; x < tiles.GetLength(0); x++) {
                for (int y = 0; y < tiles.GetLength(1); y++) {
                    if (tiles[x, y] == PLAYER || tiles[x, y] == PLAYER_OVER_GOAL)
                        return new Vector2Int(x, y);
                }
            }

            Debug.LogError("Could not find player");
            return Vector2Int.zero;
        }

        private int[,] FloodFill(char[,] tiles, Vector2Int startingPoint) {
            int width = tiles.GetLength(0);
            int height = tiles.GetLength(1);

            int[,] mask = new int[width, height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    mask[x, y] = 0;
                }
            }

            if (startingPoint.y < 0 || startingPoint.y > height - 1 || startingPoint.x < 0 || startingPoint.x > width - 1) {
                Debug.LogError("Invalid Player Position");
                return null;
            }

            Stack<Vector2Int> stack = new Stack<Vector2Int>();
            stack.Push(startingPoint);

            while (stack.Count > 0) {
                Vector2Int p = stack.Pop();
                if (p.y < 0 || p.y > height - 1 || p.x < 0 || p.x > width - 1)
                    continue;

                bool hasWall = tiles[p.x, p.y] == WALL;

                if (mask[p.x, p.y] == 0) {
                    mask[p.x, p.y] = 1;

                    if (hasWall == false) {
                        stack.Push(new Vector2Int(p.x + 1, p.y));
                        stack.Push(new Vector2Int(p.x - 1, p.y));
                        stack.Push(new Vector2Int(p.x, p.y + 1));
                        stack.Push(new Vector2Int(p.x, p.y - 1));

                        stack.Push(new Vector2Int(p.x + 1, p.y + 1));
                        stack.Push(new Vector2Int(p.x - 1, p.y - 1));
                        stack.Push(new Vector2Int(p.x - 1, p.y + 1));
                        stack.Push(new Vector2Int(p.x + 1, p.y - 1));
                    }
                }
            }

            return mask;
        }

        private int GetTileFromChar(char c) {
            switch(c) {
                case GOAL:
                    return 2;
                case PLACED_BOX:
                    return 2;
                case PLAYER_OVER_GOAL:
                    return 2;
                default:
                    return 1;
            }
        }
    }
	
}