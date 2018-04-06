using System.Collections;
using System.Collections.Generic;

namespace com.gStudios.isometric.model.world.commands {

	public class PaintTileCmd : CursorCommand {

		public PaintTileCmd(Level level, int posX, int posY, int index) : base(level, posX, posY, index) {
		}

		public override CursorCommand Excecute ()
		{
			Tile tile = level.GetTileAt (posX, posY);
			int previousIndex = tile.Type;

			if (previousIndex == Tile.EmptyTileIndex) {
				// Cannot paint an empty tile.
				return NullCommand.instance;
			}

			tile.Type = index;

			return new PaintTileCmd (level, posX, posY, previousIndex);
		}
		
	}

}