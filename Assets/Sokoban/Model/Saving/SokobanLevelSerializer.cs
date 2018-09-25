using System.Text.RegularExpressions;

using UnityEngine;

using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.saving;
using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.wall;

using com.gStudios.sokoban.model.world;

namespace com.gStudios.sokoban.model.saving {

	public class SokobanLevelSerializer : ILevelSerializer{

        const char WALL = '#';
        const char PLAYER = '@';
        const char BOX = '$';
        const char PLACED_BOX = '*';
        const char GOAL = '.';
        const char FLOOR = ' ';

        private readonly string levelData = "";

        public SokobanLevelSerializer(string levelName) {
            TextAsset levelTxt = Resources.Load(levelName) as TextAsset;

            if (levelTxt == null)
                Debug.LogError("Could not load sokoban levels.");

            levelData = levelTxt.ToString();
        }

        public bool ExistsSavedLevel() {
            return levelData != "";
        }

        public Level LoadLevel() {
            string[] lines = Regex.Split(levelData, "\r\n|\r|\n");
            int height = lines.Length - 1;
            int width = lines[0].Length;
            for (int i = 1; i < lines.Length; i++) {
                if (lines[i].Length > width)
                    width = lines[i].Length;
            }
            
            Level level = new Level(width, height);

            for (int x = 0; x < level.Width; x++) {
                for (int y = 0; y < level.Height; y++) {
                    ITile currentTile = level.GetTileAt(x, y);

                    if (x >= lines[y].Length)
                        currentTile.Type = 0;
                    else {
                        currentTile.Type = GetTileFromChar(lines[y][x]);

                        if (lines[y][x] == BOX || lines[y][x] == PLACED_BOX) {
                            currentTile.PlaceFurniture(new SokobanBox(level, currentTile));
                        }
                        else if (lines[y][x] == WALL) {
                            currentTile.PlaceFurniture(new SokobanWall(level, currentTile));
                        }
                        else if (lines[y][x] == PLAYER) {
                            level.AddCharacter(new Character(level, x, y));
                        }
                    }
                }
            }

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

        private int GetTileFromChar(char c) {
            switch(c) {
                case GOAL:
                    return 2;
                case PLACED_BOX:
                    return 2;
                default:
                    return 1;
            }
        }
    }
	
}