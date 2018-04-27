using UnityEngine;


public class PauseIfPink : MonoBehaviour
{
public RenderTexture renderTexture;

void Start ()
    {
    }

void OnPostRender ()
    {
    Texture2D tex = new Texture2D (this.renderTexture.width, this.renderTexture.height, TextureFormat.RGB24, false);
    tex.ReadPixels (new Rect (0, 0, this.renderTexture.width, this.renderTexture.height), 0, 0);
    tex.Apply ();
    foreach (Color pixel in tex.GetPixels())
        {
        if (Mathf.Approximately (1f, pixel.r) && Mathf.Approximately (0f, pixel.g) && Mathf.Approximately (1f, pixel.b))
            {
            Debug.Break ();
            }
        }

    }
}
