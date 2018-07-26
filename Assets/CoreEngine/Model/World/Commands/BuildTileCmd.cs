using System.Collections;
using System.Collections.Generic;

using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.model.world.commands {

	public class BuildTileCmd : CursorCommand {

		public BuildTileCmd(Level level, int posX, int posY, int index) : base(level, posX, posY, index) {
		}

		public override CursorCommand Excecute ()
		{
			ITile tile = level.GetTileAt (posX, posY);
			int previousIndex = tile.Type;

			if (index != TileIndex.Empty) {
				// If is building ignore already built tiles.
				if (previousIndex != TileIndex.Empty)
					return NullCommand.instance;
			}
			else {
				// If it's removing ignore empty tiles.
				if (previousIndex == TileIndex.Empty)
					return NullCommand.instance;
			}

			tile.Type = index;

			return new BuildTileCmd (level, posX, posY, previousIndex);
		}

	}

}