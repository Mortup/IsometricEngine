using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;

using com.gStudios.isometric.controller;
using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.spriteObservers;
using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.utils;

namespace com.gStudios.levelEditor.controller.cursor.modes {

	public abstract class DraggableTileMode : TileMode {

        protected Vector2Int dragStartCoords;

		Stack<GameObject> activeStaticCursors;

		public DraggableTileMode(Level level) : base(level) {
			activeStaticCursors = new Stack<GameObject> ();
        }

		public override void ClickStart (Vector2 mousePosition)
		{
			base.ClickStart (mousePosition);

			dragStartCoords = TileTransformer.ScreenToCoord(mousePosition);
        }

		public override void UpdateCursors (Vector2 mousePosition)
		{
			base.UpdateCursors (mousePosition);

			while (activeStaticCursors.Count > 0)
				SimplePool.Despawn (activeStaticCursors.Pop ());

			if (!validClickStart)
				return;

            Vector2Int startCoords = dragStartCoords;
			Vector2Int endCoords = TileTransformer.ScreenToCoord (mousePosition);

			Vector2Int minCoords = CoordUtil.MinCoords (startCoords, endCoords);
			Vector2Int maxCoords = CoordUtil.MaxCoords (startCoords, endCoords);

			GameObject cursorPrefab = Resources.Load<GameObject> (GamePaths.CursorPrefab);

			for (int x = minCoords.x; x <= maxCoords.x; x++) {
				for (int y = minCoords.y; y <= maxCoords.y; y++) {
					Vector3 pos = TileTransformer.CoordToWorld (x, y);
					GameObject staticCursorGo = SimplePool.Spawn (cursorPrefab, pos, Quaternion.identity);

					SpriteRenderer staticCursorSr = staticCursorGo.GetComponent<SpriteRenderer> ();
					staticCursorSr.sprite = GetCursorSprite (new Vector2Int(x,y));
					staticCursorSr.sortingLayerName = mainCursorSr.sortingLayerName;
					staticCursorSr.sortingOrder = SortingOrders.FloorOrder (x, y, FloorSubLayer.Cursor);

					activeStaticCursors.Push (staticCursorGo);
				}
			}

            return;
		}

		
	}

}