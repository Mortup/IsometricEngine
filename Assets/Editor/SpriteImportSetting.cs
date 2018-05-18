using UnityEngine;
using UnityEditor;

public class SpriteImportSettings : AssetPostprocessor
{
	void OnPreprocessTexture ()
	{
		TextureImporter textureImporter  = (TextureImporter) assetImporter;

		TextureImporterSettings tis = new TextureImporterSettings ();
		textureImporter.ReadTextureSettings (tis);

		tis.spritePixelsPerUnit = 64;
		tis.spriteAlignment = (int)SpriteAlignment.BottomLeft;
		tis.filterMode = FilterMode.Point;

		textureImporter.SetTextureSettings (tis);
	}
}