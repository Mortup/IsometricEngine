using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.model.characters {

	public class Character : ICharacter {

        protected Level level;

        public Character(Level level, int x, int y) {
            this.level = level;
            this.X = x;
            this.Y = y;
        }

        public int X { get; protected set; }

        public int Y { get; protected set; }

        public void Walk(int xOffset, int yOffset) {
            ITile destinationTile = level.GetTileAt(X + xOffset, Y + yOffset);
            WalkInfo walkInfo = new WalkInfo(xOffset, yOffset);

            if (destinationTile.IsWalkable(walkInfo)) {
                this.X += xOffset;
                this.Y += yOffset;

                // OnStandOver Callback
                destinationTile.OnStandOver(walkInfo);
            }

        }

    }

}