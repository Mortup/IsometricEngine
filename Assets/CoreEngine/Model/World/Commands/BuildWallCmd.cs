﻿using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.model.world.commands {

    public class BuildWallCmd : AbstractWorldCommand {

        int posZ;

        public BuildWallCmd(Level level, int posX, int posY, int posZ, int index) : base(level, posX, posY, index) {
            this.posZ = posZ;
        }

        public override IWorldCommand Excecute() {
            IWall wall = level.GetWallAt(posX, posY, posZ);
            int previousIndex = wall.Type;

            if (index != WallIndex.Empty) {
                // If is building ignore already built walls.
                if (previousIndex != WallIndex.Empty)
                    return NullCommand.instance;
            }
            else {
                // If it's removing ignore empty tiles.
                if (previousIndex == WallIndex.Empty)
                    return NullCommand.instance;
            }

            wall.Type = index;

            return new BuildWallCmd(level, posX, posY, posZ, previousIndex);
        }
    }

}