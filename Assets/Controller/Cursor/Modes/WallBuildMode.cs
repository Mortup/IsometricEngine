using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.data;
using com.gStudios.isometric.controller.spriteObservers;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor.modes {

	public class WallBuildMode : DefaultMode {

        Stack<GameObject> activeStaticCursors;
        Vector2Int dragStartVertexCoords;

        public WallBuildMode(Level level) : base(level) {
            activeStaticCursors = new Stack<GameObject>();

            mainCursorSr.sortingLayerName = "Tiles";
            mainCursorSr.sprite = DataManager.cursorSpriteData.wallMainSprite;
        }

        protected override CursorCommand GetActionCommand(Vector2 mousePosition) {
            return NullCommand.instance;
        }

        public override void ClickStart(Vector2 mousePosition) {
            base.ClickStart(mousePosition);

            dragStartVertexCoords = IsometricTransformer.ScreenToVertex(mousePosition);
        }

        public override void UpdateCursors(Vector2 mousePosition) {
            Vector2Int vertexCoords = IsometricTransformer.ScreenToVertex(mousePosition);

            // Main cursor
            if (level.IsVertexInBounds(vertexCoords.x, vertexCoords.y)) {
                mainCursorSr.enabled = true;
                mainCursorSr.sortingOrder = TileSpriteObserver.GetSortingOrder(vertexCoords.x, vertexCoords.y) + mainCursorSortingOrderOffset; // Only for tiles?

                Vector3 pos = IsometricTransformer.VertexToWorld(vertexCoords);
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

            GameObject cursorPrefab = Resources.Load<GameObject>(GamePaths.CursorPrefab);
            GameObject staticCursorGo = SimplePool.Spawn(cursorPrefab, IsometricTransformer.VertexToWorld(dragStartVertexCoords), Quaternion.identity);

            SpriteRenderer staticCursorSr = staticCursorGo.GetComponent<SpriteRenderer>();
            staticCursorSr.sprite = DataManager.cursorSpriteData.wallMainSprite;
            staticCursorSr.sortingLayerName = mainCursorSr.sortingLayerName;
            staticCursorSr.sortingOrder = TileSpriteObserver.GetSortingOrder(dragStartVertexCoords.x, dragStartVertexCoords.y) + mainCursorSortingOrderOffset;

            activeStaticCursors.Push(staticCursorGo);
        }
    }

}