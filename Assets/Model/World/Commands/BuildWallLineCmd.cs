using System;
using System.Collections.Generic;

namespace com.gStudios.isometric.model.world.commands {

	public class BuildWallLineCmd : CompositeCommand {

        /// <summary>
        /// Receives vertex coordinates.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <param name="posZ"></param>
        /// <param name="index"></param>
        public BuildWallLineCmd(Level level, int startX, int startY, int endX, int endY, int posZ, int index) :
            base(level, CreateLine(level, startX, startY, endX, endY, posZ, index)) { }

        static List<CursorCommand> CreateLine(Level level, int startX, int startY, int endX, int endY, int posZ, int index) {
            int minX = Math.Min(startX, endX);
            int maxX = Math.Max(startX, endX);
            int minY = Math.Min(startY, endY);
            int maxY = Math.Max(startY, endY);

            int diffX = maxX - minX;
            int diffY = maxY - minY;

            if (diffX > diffY)
                maxX -= 1;
            else
                maxY -= 1;

            List<CursorCommand> commands = new List<CursorCommand>();
            for (int x = minX; x <= maxX; x++) {
                for (int y = minY; y <= maxY; y++) {
                    commands.Add(new BuildWallCmd(level, x, y, posZ, index));
                }

            }

            return commands;
        }
		
	}

}