using UnityEngine;

namespace GGEZ
{


[
    HelpURL ("http://ggez.org/posts/perfect-pixel-sprite/"), // Website opened by clicking the book icon on the component
    DisallowMultipleComponent,                               // Only one of these per GameObject
    AddComponentMenu ("GGEZ/Sprite/Perfect Pixel Sprite")    // Insert into the "Add Component..." menu
]
public class PerfectPixelSprite : MonoBehaviour
{

// Set this value to the same value as Pixels Per Unit when importing sprites
[
    Tooltip ("The number of texture pixels that fit in 1.0 world units. Common values are 8, 16, 32 and 64. If you're making a tile-based game, this is your tile size."),
    Range (1, 64)
]
public int TexturePixelsPerWorldUnit = 16;


//---------------------------------------------------------------------------
// OnEnable - Called by Unity when the component is created or enabled.
// Check to make sure this component has been added to a valid object.
//---------------------------------------------------------------------------
void OnEnable ()
    {
    if (this.transform.parent == null)
        {
#if UNITY_EDITOR
        Debug.LogError ("PerfectPixelSprite needs to be on an object with a parent. It makes adjustments to localPosition based on the parent's position. The GameObject structure should be [Prefab] contains [PerfectPixelSprite] which contains one or more [SpriteRenderer]", this);
#endif
        this.enabled = false;
        }

#if UNITY_EDITOR
    // Help out by warning about an incompatible camera in Editor mode
    if (!Camera.main.orthographic)
        {
        Debug.LogWarning ("Camera is not in orthographic mode", this);
        }
#endif

    }

//---------------------------------------------------------------------------
// OnDisable - Called by Unity when the component is disabled or destroyed.
// This is used to clean up our changes to localPosition.
//---------------------------------------------------------------------------
void OnDisable ()
    {
    this.transform.localPosition = Vector3.zero;
    }

//---------------------------------------------------------------------------
// LateUpdate - Called by Unity after all other functions have run Update.
// This should run after the parent's position has been updated. If you
// change the parent's position in other LateUpdate functions, use the Script
// Execution Order project setting to make this script run last.
//---------------------------------------------------------------------------
void LateUpdate ()
    {

    // Get a local reference
    Camera camera = Camera.main;

    // Compute how zoomed this camera is so that we can compute how big screen
    // pixels are in world-space. We always snap to screen pixels since one
    // of two situations is true:
    //  1: World pixels are smaller. In this case, snapping to them creates
    //     shimmering artifacts, so you don't want to do it.
    //  2: World pixels are equal or bigger. In this case, you get smoother
    //     motion by snapping to screen-pixels.
    float texturePixelsPerWorldUnit = this.TexturePixelsPerWorldUnit;
    float snapSizeWorldUnits =
        1f / (Mathf.Max (1f, Mathf.Ceil ((1f * camera.pixelRect.height) / (camera.orthographicSize * 2f * texturePixelsPerWorldUnit))) * texturePixelsPerWorldUnit);

    // Offset children from the parent's position
    var parentPosition = this.transform.parent.position;
    this.transform.localPosition = new Vector3 (
            -repeatUniform (parentPosition.x, snapSizeWorldUnits),
            snapSizeWorldUnits*0.5f-repeatUniform (parentPosition.y, snapSizeWorldUnits),
            0f
            );
	}

//---------------------------------------------------------------------------
// repeatUniform - Similar to Mathf.Repeat, but doesn't mirror when crossing
// from positive to negative. See: http://github.com/karlgluck/ggez-labkit
//
// Normally:
//       [4 .. -4] % 3 ==> [1, 0, 2, 1, 0, -1, -2, 0, -1]
//
// Instead:
//       repeatUniform ([4 .. -4], 3) ==> [1, 0, 2, 1, 0, 2, 1, 0, 2]
//
// Using repeatUniform makes the zero-boundary disappear.
//---------------------------------------------------------------------------
private static float repeatUniform (float number, float range)
    {
    var retval = number - ((int)(number/range))*range;
    if (retval < 0)
        {
        retval += range;
        }
    return retval;
    }

}

}
