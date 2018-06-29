using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.gStudios.isometric.controller.config;

namespace com.gStudios.isometric.controller.data {

	public class CursorSpriteDataLoader {

		public Sprite defaultSprite;
		public Sprite emptySprite;

		public Sprite tileBuildSprite;
		public Sprite tileRemoveSprite;
		public Sprite tileBuildOverTileSprite;
		public Sprite tileRemoveOnEmptySprite;

        public Sprite wallMainSprite;
        public Sprite wallBuildSprite;
        public Sprite wallBulldozeSprite;

		public CursorSpriteDataLoader() {
			defaultSprite = Resources.Load<Sprite> (GamePaths.CursorSprite ("Default"));
			emptySprite = Resources.Load<Sprite> (GamePaths.TileCursorSprite ("Empty"));

			tileBuildSprite = Resources.Load<Sprite> (GamePaths.TileCursorSprite ("Build"));
			tileRemoveSprite = Resources.Load<Sprite> (GamePaths.TileCursorSprite ("BuildInverse"));
			tileBuildOverTileSprite = Resources.Load<Sprite> (GamePaths.TileCursorSprite ("BuildOverTile"));
            tileRemoveOnEmptySprite = Resources.Load<Sprite> (GamePaths.TileCursorSprite ("BuildInverseOnEmpty"));

            wallMainSprite = Resources.Load<Sprite>(GamePaths.WallCursorSprite("Main"));
            wallBuildSprite = Resources.Load<Sprite>(GamePaths.WallCursorSprite("Build"));
            wallBulldozeSprite = Resources.Load<Sprite>(GamePaths.WallCursorSprite("Bulldoze"));
        }
		
	}

}