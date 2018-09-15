using System;
using System.Text.RegularExpressions;

using UnityEngine;

using com.gStudios.isometric.model.saving;
using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.wall;

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
                    if (x >= lines[y].Length)
                        level.GetTileAt(x, y).Type = 0;
                    else
                        level.GetTileAt(x, y).Type = GetTileFromChar(lines[y][x]);
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
                case WALL:
                    return 1;
                default:
                    return 0;
            }
        }
    }
	
}