using UnityEngine;
using UnityEditor;

using com.gStudios.isometric.controller.config;

public class SpritePostProcessor : AssetPostprocessor
{
    private int PPU = Settings.PPU;
    

	void OnPreprocessTexture ()
	{
		TextureImporter textureImporter  = (TextureImporter) assetImporter;

		TextureImporterSettings tis = new TextureImporterSettings ();
		textureImporter.ReadTextureSettings (tis);

		tis.spritePixelsPerUnit = PPU;
		tis.filterMode = Settings.filterMode;
		tis.mipmapEnabled = Settings.mipmapEnabled;
        tis.spriteMode = (int)SpriteImportMode.Single;
        tis.wrapMode = Settings.wrapMode;

        if (assetPath.Contains("Tiles") || assetPath.Contains("TileCursors")) {
			tis.spriteAlignment = (int)SpriteAlignment.Custom;
            tis.spritePivot = Settings.tilePivot;
		}
        else if (assetPath.Contains("Walls")) {
            tis.spriteAlignment = (int)SpriteAlignment.Custom;
            tis.spritePivot = Settings.wallPivot;
        }
        else if (assetPath.Contains("Furniture") || assetPath.Contains("Characters")) {
            tis.spriteAlignment = (int)SpriteAlignment.Custom;
            tis.spritePivot = Settings.furniturePivot;
        }
        else if (assetPath.Contains("WallCursors")) {
            tis.spriteAlignment = (int)SpriteAlignment.Custom;
            tis.spritePivot = Settings.wallCursorPivot;
        }

		textureImporter.SetTextureSettings (tis);
	}

}