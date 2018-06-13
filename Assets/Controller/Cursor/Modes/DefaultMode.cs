using UnityEngine;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.commands;

using com.gStudios.isometric.controller.data;

namespace com.gStudios.isometric.controller.cursor.modes {

    /// <summary>
    /// Base abstract class for all cursor modes.
    /// Provides basic functionality.
    /// </summary>
	public abstract class DefaultMode : CursorMode {

		protected int mainCursorSortingOrderOffset = 3;

		protected Level level;
		protected int index = -1;   // The current index of the mode (May be the wall painting index, 0 for removing and 1 for bulding, etc.)

		protected GameObject mainCursorGo;
		protected SpriteRenderer mainCursorSr;

        protected bool validClickStart = false; // Has the user started the click in a valid way?

		public DefaultMode(Level level) {
			this.level = level;

			mainCursorGo = new GameObject ("Main Cursor");
			mainCursorSr = mainCursorGo.AddComponent<SpriteRenderer> ();
			mainCursorSr.sortingLayerName = "Debug";
        }

		public virtual void Activate() {
			mainCursorGo.SetActive (true);
		}
		public virtual void Deactivate() {
            GameObject.Destroy(mainCursorGo);
		}

		public virtual void ClickStart (Vector2 mousePosition) {
            validClickStart = true;
        }

		public CursorCommand ClickEnd (Vector2 mousePosition) {
            if (!validClickStart)
                return NullCommand.instance;

            CursorCommand action = GetActionCommand(mousePosition);

            validClickStart = false;

            return action;
        }

        protected abstract CursorCommand GetActionCommand(Vector2 mousePosition);

        public abstract void UpdateCursors(Vector2 mousePosition);

		public virtual void SetIndex(int index) {
			this.index = index;
		}

	}

}