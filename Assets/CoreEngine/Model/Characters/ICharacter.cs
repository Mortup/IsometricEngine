using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.model.characters {

	public interface ICharacter {

        Level Level { get; }

        float X { get; }
        float Y { get; }

        int roundedX { get; }
        int roundedY { get; }

        void Walk(float xOffset, float yOffset);

        void Subscribe(ICharacterObserver observer);
	}

}