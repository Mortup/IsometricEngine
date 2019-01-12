using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.commands;
using com.gStudios.isometric.model.world.furniture;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.levelEditor.controller.cursor.modes {

    public class RemoveFurnitureCommand : IWorldCommand {

        private Level level;
        private int posX;
        private int posY;

        public RemoveFurnitureCommand(Level level, int posX, int posY) {
            // Rotation should be saved here.

            this.level = level;
            this.posX = posX;
            this.posY = posY;
        }

        public IWorldCommand Excecute() {
            ITile tile = level.GetTileAt(posX, posY);

            if (tile.HasFurniture() == false)
                return NullCommand.instance;

            IFurniture previousFurniture = tile.GetPlacedFurniture();
            tile.RemoveFurniture();

            return new PlaceFurnitureCommand(level, posX, posY, previousFurniture);
        }

    }
}