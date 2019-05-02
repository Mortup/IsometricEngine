using System;

namespace com.gStudios.isometric.model.saving {

    [Serializable]
    public class LevelData {
        public int width;
        public int height;

        public int[] tiles;
        public int[] wallIndexes;
        public int[] furnitureIndexes;
    }

}