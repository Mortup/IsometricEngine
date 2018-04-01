using System;
using System.Collections;
using System.Collections.Generic;

namespace com.gStudios.isometric.model.world.commands {

	public class BuildAreaCmd : CompositeCommand {

		public BuildAreaCmd(Level level, int startX, int endX, int startY, int endY, int index) :
		base(level, CreateArea(level, startX, endX, startY, endY, index)) {}

		static List<CursorCommand> CreateArea(Level level, int startX, int endX, int startY, int endY, int index) {
			int minX = Math.Min (startX, endX);
			int maxX = Math.Max (startX, endX);
			int minY = Math.Min (startY, endY);
			int maxY = Math.Max (startY, endY);

			List<CursorCommand> commands = new List<CursorCommand> ();
			for (int x = minX; x <= maxX; x++) {
				for (int y = minY; y <= maxY; y++) {
					commands.Add (new BuildTileCmd (level, x, y, index));
				}

			}

			return commands;
		}
		
	}

}