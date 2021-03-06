﻿using System;
using System.Collections.Generic;

namespace com.gStudios.isometric.model.world.commands {

	public class PaintAreaCmd : CompositeCommand {

		public PaintAreaCmd(Level level, int startX, int endX, int startY, int endY, int index) :
		base(level, CreateArea(level, startX, endX, startY, endY, index)) {}

		static List<IWorldCommand> CreateArea(Level level, int startX, int endX, int startY, int endY, int index) {
			int minX = Math.Min (startX, endX);
			int maxX = Math.Max (startX, endX);
			int minY = Math.Min (startY, endY);
			int maxY = Math.Max (startY, endY);

			List<IWorldCommand> commands = new List<IWorldCommand> ();
			for (int x = minX; x <= maxX; x++) {
				for (int y = minY; y <= maxY; y++) {
					commands.Add (new PaintTileCmd (level, x, y, index));
				}

			}

			return commands;
		}

	}

}