using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.model.characters {

	public interface ICharacter {

        Level Level { get; }

        float x { get; }
        float y { get; }

        int roundedX { get; }
        int roundedY { get; }

        float width { get; }
        float height { get; }

        void Walk(float xOffset, float yOffset);

        void Subscribe(ICharacterObserver observer);
	}

}