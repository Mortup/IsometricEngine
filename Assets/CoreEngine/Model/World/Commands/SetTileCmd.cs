using System.Collections;
using System.Collections.Generic;

using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.model.world.commands {

	public class SetTileCmd : CursorCommand {

		public SetTileCmd(Level level, int posX, int posY, int index) : base(level, posX, posY, index) {
		}

		public override CursorCommand Excecute ()
		{
			if (index == TileIndex.Empty) {
				// Cannot destroy tiles.
				return NullCommand.instance;
			}

			ITile tile = level.GetTileAt (posX, posY);
			int previousIndex = tile.Type;

			if (previousIndex == TileIndex.Empty)
				// Cannot paint empty tiles.
				return NullCommand.instance;

			tile.Type = index;

			return new SetTileCmd (level, posX, posY, previousIndex);
		}
		
	}

}