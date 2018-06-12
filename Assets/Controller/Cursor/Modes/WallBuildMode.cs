using UnityEngine;

using com.gStudios.isometric.controller.data;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;

namespace com.gStudios.isometric.controller.cursor.modes {

	public class WallBuildMode : DefaultMode {

        public WallBuildMode(Level level) : base(level) {
            mainCursorSr.sortingLayerName = "Tiles";

            defaultSprite = DataManager.cursorSpriteData.wallMainSprite;
            Debug.Log(DataManager.cursorSpriteData.wallMainSprite);
        }

        protected override CursorCommand GetActionCommand(Vector2 mousePosition) {
            throw new System.NotImplementedException();
        }
    }

}