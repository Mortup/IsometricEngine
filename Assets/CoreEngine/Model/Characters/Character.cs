namespace com.gStudios.isometric.model.characters {

	public class Character : ICharacter {

        public Character(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        public int X { get; protected set; }

        public int Y { get; protected set; }

        public void Walk(int xOffset, int yOffset) {
            this.X += xOffset;
            this.Y += yOffset;
        }

    }

}