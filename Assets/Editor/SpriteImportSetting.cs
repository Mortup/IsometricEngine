using UnityEngine;
using UnityEditor;

using com.gStudios.isometric.controller;

public class SpriteImportSettings : AssetPostprocessor
{
	public const int PPU = 64;

	void OnPreprocessTexture ()
	{
		TextureImporter textureImporter  = (TextureImporter) assetImporter;

		TextureImporterSettings tis = new TextureImporterSettings ();
		textureImporter.ReadTextureSettings (tis);

		tis.spritePixelsPerUnit = PPU;
		tis.filterMode = FilterMode.Point;
		tis.mipmapEnabled = false;
        tis.spriteMode = (int)SpriteImportMode.Single;

        if (assetPath.Contains("Tiles") || assetPath.Contains("Cursors")) {
			float tileHeightOffset = ((float)26/42);

			tis.spriteAlignment = (int)SpriteAlignment.Custom;
			tis.spritePivot = new Vector2 (0, tileHeightOffset);
		}
		else if (assetPath.Contains("Walls")) {
			float tileHeightOffset = ((float)2 / 130);
			float tileWidthOffset = ((float)2 / 38);

			tis.spriteAlignment = (int)SpriteAlignment.Custom;
			tis.spritePivot = new Vector2 (tileWidthOffset, tileHeightOffset);
		}

		textureImporter.SetTextureSettings (tis);
	}

}