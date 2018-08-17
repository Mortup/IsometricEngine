namespace com.gStudios.isometric.model.characters {

	public class Character : ICharacter {

        public Character(int x, int y) {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }

    }

}