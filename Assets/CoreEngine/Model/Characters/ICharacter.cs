namespace com.gStudios.isometric.model.characters {

	public interface ICharacter {

        int X { get; }
        int Y { get; }

        void Walk(int xOffset, int yOffset);
		
	}

}