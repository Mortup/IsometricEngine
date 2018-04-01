using System.Collections;
using System.Collections.Generic;

namespace com.gStudios.isometric.model.world.commands {

	public class SetTileCmd : CursorCommand {

		public SetTileCmd(Level level, int posX, int posY, int index) : base(level, posX, posY, index) {
		}

		public override CursorCommand Excecute ()
		{
			if (index == Tile.EmptyTileIndex) {
				// Cannot destroy tiles.
				return NullCommand.instance;
			}

			Tile tile = level.GetTileAt (posX, posY);
			int previousIndex = tile.Type;

			if (previousIndex == Tile.EmptyTileIndex)
				// Cannot paint empty tiles.
				return NullCommand.instance;

			tile.Type = index;

			return new SetTileCmd (level, posX, posY, previousIndex);
		}
		
	}

}