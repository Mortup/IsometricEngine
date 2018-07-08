using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.levelEditor.controller.cursor.modes {

	public class WallBuildMode : DefaultMode {

        Stack<GameObject> activeStaticCursors;
        Vector2Int dragStartVertexCoords;

        public WallBuildMode(Level level) : base(level) {
            activeStaticCursors = new Stack<GameObject>();

            mainCursorSr.sortingLayerName = "Tiles";
        }

        protected override CursorCommand GetActionCommand(Vector2 mousePosition) {
            int selectedIndex = Input.GetButton("InverseFunction") ? WallIndex.EmptyWallIndex : WallIndex.NewWallIndex;
            Vector2Int vertexCoords = VertexTransfomer.ScreenToVertex(mousePosition);

            Vector2Int diff = new Vector2Int(Mathf.Abs(vertexCoords.x - dragStartVertexCoords.x), Mathf.Abs(vertexCoords.y - dragStartVertexCoords.y));
            Vector2Int endDragCoords = vertexCoords;

            int posZ;
            if (diff.x > diff.y) {
                endDragCoords.y = dragStartVertexCoords.y;
                posZ = 0;
            }
            else {
                endDragCoords.x = dragStartVertexCoords.x;
                posZ = 1;
            }

            return new BuildWallLineCmd(level, dragStartVertexCoords.x, dragStartVertexCoords.y, endDragCoords.x, endDragCoords.y, posZ, selectedIndex);
        }

        public override void ClickStart(Vector2 mousePosition) {
            base.ClickStart(mousePosition);

            dragStartVertexCoords = VertexTransfomer.ScreenToVertex(mousePosition);
        }

        public override void UpdateCursors(Vector2 mousePosition) {
            Vector2Int vertexCoords = VertexTransfomer.ScreenToVertex(mousePosition);

            // Main cursor
            if (level.IsVertexInBounds(vertexCoords.x, vertexCoords.y)) {
                mainCursorSr.enabled = true;
                mainCursorSr.sortingOrder = WallSpriteObserver.GetSortingOrder(vertexCoords.x, vertexCoords.y, 0, TileSubLayer.SecondWallCursor);
                mainCursorSr.sprite = Input.GetButton("InverseFunction") ? DataManager.cursorSpriteData.wallBulldozeSprite : DataManager.cursorSpriteData.wallBuildSprite;

                Vector3 pos = VertexTransfomer.VertexToWorld(vertexCoords);
                mainCursorGo.transform.position = new Vector3(pos.x, pos.y, 0f);
            }
            else {
                mainCursorSr.enabled = false;
            }

            // Static cursors
            while (activeStaticCursors.Count > 0)
                SimplePool.Despawn(activeStaticCursors.Pop());

            if (!validClickStart)
                return;

            Vector2Int diff = new Vector2Int(Mathf.Abs(vertexCoords.x - dragStartVertexCoords.x), Mathf.Abs(vertexCoords.y - dragStartVertexCoords.y));
            Vector2Int endDragCoords = vertexCoords;
            if (diff.x > diff.y) {
                endDragCoords.y = dragStartVertexCoords.y;
            }
            else {
                endDragCoords.x = dragStartVertexCoords.x;
            }

            GameObject cursorPrefab = Resources.Load<GameObject>(GamePaths.CursorPrefab);

            for (int x = Mathf.Min(dragStartVertexCoords.x, endDragCoords.x); x <= Mathf.Max(dragStartVertexCoords.x, endDragCoords.x); x++) {
                for (int y = Mathf.Min(dragStartVertexCoords.y, endDragCoords.y); y <= Mathf.Max(dragStartVertexCoords.y, endDragCoords.y); y++) {
                    Vector2Int currentCoord = new Vector2Int(x, y);

                    GameObject staticCursorGo = SimplePool.Spawn(cursorPrefab, VertexTransfomer.VertexToWorld(currentCoord), Quaternion.identity);

                    SpriteRenderer staticCursorSr = staticCursorGo.GetComponent<SpriteRenderer>();
                    staticCursorSr.sprite = Input.GetButton("InverseFunction") ? DataManager.cursorSpriteData.wallBulldozeSprite : DataManager.cursorSpriteData.wallBuildSprite;
                    staticCursorSr.sortingLayerName = mainCursorSr.sortingLayerName;
                    staticCursorSr.sortingOrder = WallSpriteObserver.GetSortingOrder(currentCoord.x, currentCoord.y, 0, TileSubLayer.FirstWallCursor);

                    activeStaticCursors.Push(staticCursorGo);
                }
            }

            return;
        }
    }

}