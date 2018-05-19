using UnityEngine;
using UnityEditor;

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

		Debug.Log (assetPath);
		if (assetPath.Contains("Tiles") || assetPath.Contains("Cursors")) {
			Debug.Log ("Contains");
			tis.spriteAlignment = (int)SpriteAlignment.Custom;
			tis.spritePivot = new Vector2 (0, 0.6f);
		}

		textureImporter.SetTextureSettings (tis);
	}
}