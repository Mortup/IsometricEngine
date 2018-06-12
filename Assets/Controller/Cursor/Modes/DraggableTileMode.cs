using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.spriteObservers;
using com.gStudios.utils;

namespace com.gStudios.isometric.controller.cursor.modes {

	public abstract class DraggableTileMode : TileMode {

		const int staticOrderOffset = 1;        // Sprite order in layer for static cursors.

		protected bool isDragging = false;
        protected Vector2Int dragStartCoords;

		Stack<GameObject> activeStaticCursors;

		public DraggableTileMode(Level level) : base(level) {
			activeStaticCursors = new Stack<GameObject> ();
        }

		public override void ClickStart (Vector2 mousePosition)
		{
			base.ClickStart (mousePosition);

			isDragging = true;
			dragStartCoords = IsometricTransformer.ScreenToCoord(mousePosition);
        }

		public override void UpdateCursors (Vector2 mousePosition)
		{
			base.UpdateCursors (mousePosition);

			while (activeStaticCursors.Count > 0)
				SimplePool.Despawn (activeStaticCursors.Pop ());

			if (!isDragging)
				return;

            Vector2Int startCoords = dragStartCoords;
			Vector2Int endCoords = IsometricTransformer.ScreenToCoord (mousePosition);

			Vector2Int minCoords = CoordUtil.MinCoords (startCoords, endCoords);
			Vector2Int maxCoords = CoordUtil.MaxCoords (startCoords, endCoords);

			GameObject cursorPrefab = Resources.Load<GameObject> (GamePaths.CursorPrefab);

			for (int x = minCoords.x; x <= maxCoords.x; x++) {
				for (int y = minCoords.y; y <= maxCoords.y; y++) {
					Vector3 pos = IsometricTransformer.CoordToWorld (x, y);
					GameObject staticCursorGo = SimplePool.Spawn (cursorPrefab, pos, Quaternion.identity);

					SpriteRenderer staticCursorSr = staticCursorGo.GetComponent<SpriteRenderer> ();
					staticCursorSr.sprite = GetCursorSprite (new Vector2Int(x,y));
					staticCursorSr.sortingLayerName = mainCursorSr.sortingLayerName;
					staticCursorSr.sortingOrder = TileSpriteObserver.GetSortingOrder (x, y) + staticOrderOffset;

					activeStaticCursors.Push (staticCursorGo);
				}
			}
		}

		
	}

}