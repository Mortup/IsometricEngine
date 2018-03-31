using System.Collections;
using System.Collections.Generic;

namespace com.gStudios.isometric.model.world.commands {

	public abstract class CursorCommand {

		protected Level level;
		protected int posX, posY;
		protected int index;

		public CursorCommand(Level level, int posX, int posY, int index) {
			this.level = level;
			this.posX = posX;
			this.posY = posY;
			this.index = index;
		}

		/// <summary>
		/// Excecutes the current command action.
		/// Returns the reversed command.
		/// </summary>
		public abstract CursorCommand Excecute ();

	}

}