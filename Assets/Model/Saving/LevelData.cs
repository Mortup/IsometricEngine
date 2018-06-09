using System;
using System.Collections;
using System.Collections.Generic;

using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.model.saving {

	[Serializable]
	public class LevelData
	{
		public int width;
		public int height;

        public int[] tiles;
        public int[] wallIndexes;
    }

}