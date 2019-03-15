using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.model.world;
using UnityEngine;
using UnityEngine.UI;

namespace com.gStudios.isometric.controller {

    class DebugInfoController : MonoBehaviour, ILevelController {
        [SerializeField] Text debugTextField;
        [Header("Settings")]
        [SerializeField] bool showMouseCoordinates;

        CoreLevelController clc;
        Level level;

        public void Init(CoreLevelController clc) {
            this.clc = clc;
        }

        public void OnLevelInit(Level level) {
            this.level = level;
        }

        public void Update() {
            if (showMouseCoordinates) {
                Vector2Int tileCoords = TileTransformer.ScreenToCoord(Input.mousePosition);
                Vector3Int wallCoords = WallTransformer.ScreenToCoord(Input.mousePosition);
                debugTextField.text = string.Format(
                    "Tile Coord: {0}, {1}\nWall Coord: {2}, {3}, {4}\nOrientation: {5}",
                    tileCoords.x,
                    tileCoords.y,
                    wallCoords.x,
                    wallCoords.y,
                    wallCoords.z,
                    OrientationManager.currentOrientation);
            }
            else {
                debugTextField.text = "";
            }
        }

    }

}