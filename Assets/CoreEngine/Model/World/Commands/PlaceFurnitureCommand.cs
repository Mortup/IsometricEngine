using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;
using com.gStudios.isometric.model.world.furniture;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.levelEditor.controller.cursor.modes {

    public class PlaceFurnitureCommand : IWorldCommand {

        private Level level;
        private int posX;
        private int posY;

        private IFurniture furniture;

        public PlaceFurnitureCommand(Level level, int posX, int posY, IFurniture furniture) {
            // Rotation should be saved here.

            this.level = level;
            this.posX = posX;
            this.posY = posY;
            this.furniture = furniture;
        }

        public IWorldCommand Excecute() {
            ITile tile = level.GetTileAt(posX, posY);

            if (tile.HasFurniture())
                return NullCommand.instance;

            tile.PlaceFurniture(furniture);
            return new RemoveFurnitureCommand(level, posX, posY);
        }

    }
}