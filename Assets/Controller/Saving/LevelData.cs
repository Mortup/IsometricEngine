using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller.saving {

	[Serializable]
	public class LevelData
	{
		public int[] tiles;
		public int width;
		public int height;

	}

}