using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.data.structs;
using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.levelEditor.controller.cursor.modes {

    public class WallPaintMode : DefaultMode {

        Dictionary<Vector3Int, GameObject> selectedPositions;

        GameObject arrowCursor;

        public WallPaintMode(Level level) : base(level) {
            selectedPositions = new Dictionary<Vector3Int, GameObject>();
            SetIndex(WallIndex.New + 1);

            mainCursorSr.sortingLayerName = "Tiles";

            arrowCursor = new GameObject("WallPaint Arrow Cursor");
            SpriteRenderer arrowCursorSr = arrowCursor.AddComponent<SpriteRenderer>();
            arrowCursorSr.sprite = DataManager.cursorSpriteData.wallMainSprite;
            arrowCursorSr.sortingLayerName = "Tiles";
        }

        public override void Deactivate() {
            base.Deactivate();
            GameObject.Destroy(arrowCursor);
        }

        public override void UpdateCursors(Vector2 mousePosition) {
            base.UpdateCursors(mousePosition);

            Vector3Int currentWallPos = WallTransformer.ScreenToCoord(mousePosition);
            Vector2 worldPos = WallTransformer.CoordToWorld(currentWallPos);

            // Static cursors
            if (validClickStart) {               

                GameObject cursorPrefab = Resources.Load<GameObject>(GamePaths.CursorPrefab);
                if (selectedPositions.ContainsKey(currentWallPos) == false && IsWallAt(currentWallPos)) {
                    GameObject staticCursor = SimplePool.Spawn(cursorPrefab, worldPos, Quaternion.identity);

                    SpriteRenderer staticCursorSr = staticCursor.GetComponent<SpriteRenderer>();
                    staticCursorSr.sprite = GetCursorSprite(currentWallPos);
                    staticCursorSr.sortingLayerName = mainCursorSr.sortingLayerName;
                    staticCursorSr.sortingOrder = WallSpriteObserver.GetSortingOrder(currentWallPos.x, currentWallPos.y, currentWallPos.z, TileSubLayer.FirstWallCursor);

                    selectedPositions.Add(currentWallPos, staticCursor);
                }
            }
            // Main cursor
            else {
                if (IsWallAt(currentWallPos)) {

                    mainCursorSr.enabled = true;
                    mainCursorSr.sortingOrder = WallSpriteObserver.GetSortingOrder(currentWallPos.x, currentWallPos.y, currentWallPos.z, TileSubLayer.FirstWallCursor);
                    mainCursorSr.sprite = GetCursorSprite(currentWallPos);

                    mainCursorGo.transform.position = worldPos;
                }
                else {
                    mainCursorSr.enabled = false;
                }
            }

            // Arrow cursor
            arrowCursor.transform.position = WallTransformer.CoordToWorld(currentWallPos) + new Vector2(Settings.TILE_WIDTH / 4, Settings.TILE_HEIGHT / 4);
            arrowCursor.GetComponent<SpriteRenderer>().sortingOrder = WallSpriteObserver.GetSortingOrder(currentWallPos.x, currentWallPos.y, currentWallPos.z, TileSubLayer.SecondWallCursor);
        }

        protected override CursorCommand GetActionCommand(Vector2 mousePosition) {
            int selectedIndex = Input.GetButton("InverseFunction") ? WallIndex.New : index;

            List<CursorCommand> commands = new List<CursorCommand>();
            foreach (KeyValuePair<Vector3Int, GameObject> entry in selectedPositions) {
                Vector3Int pos = entry.Key;
                commands.Add(new PaintWallCmd(level, pos.x, pos.y, pos.z, selectedIndex));
            }
                CleanCursors();
            return new CompositeCommand(level, commands);
        }

        private Sprite GetCursorSprite(Vector3Int wallCoords) {
            IWall wallToGetSprite = new NullWall(level, wallCoords.x, wallCoords.y, wallCoords.z);
            IWallSprite wallSprite = Input.GetButton("InverseFunction") ? DataManager.wallSpriteData.GetDataById(WallIndex.New) : DataManager.wallSpriteData.GetDataById(index);
            return wallSprite.GetSprite(wallToGetSprite, false);
        }

        private bool IsWallAt(Vector3Int wallPos) {
            return level.IsWallInBounds(wallPos.x, wallPos.y, wallPos.z) && level.GetWallAt(wallPos.x, wallPos.y, wallPos.z).Type != WallIndex.Empty;
        }

        private void CleanCursors() {
            foreach(KeyValuePair<Vector3Int, GameObject> entry in selectedPositions) {
                GameObject.Destroy(entry.Value);
            }

            selectedPositions.Clear();
        }
    }

}