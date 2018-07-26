using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.model.world.commands {

    public class PaintWallCmd : CursorCommand {

        int posZ;

        public PaintWallCmd(Level level, int posX, int posY, int posZ, int index) : base(level, posX, posY, index) {
            this.posZ = posZ;
        }

        public override CursorCommand Excecute() {
            IWall wall = level.GetWallAt(posX, posY, posZ);
            int previousIndex = wall.Type;

            if (previousIndex == WallIndex.Empty) {
                // Cannot paint an empty wall.
                return NullCommand.instance;
            }

            wall.Type = index;

            return new PaintWallCmd(level, posX, posY, posZ, previousIndex);
        }
    }

}