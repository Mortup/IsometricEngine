using UnityEngine;

using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller.camera {

    public class CameraController : MonoBehaviour, ILevelController {

        [SerializeField] private Camera mainCamera;

        private CameraDrag cameraDrag;
        private CameraFollowRotation cameraRotation;
        private CameraZoom cameraZoom;

        public void Init(Level level) {
            Debug.Log("Being init");

            if (cameraDrag == null) {
                cameraDrag = mainCamera.gameObject.AddComponent<CameraDrag>();
            }

            if (cameraRotation == null) {
                cameraRotation = mainCamera.gameObject.AddComponent<CameraFollowRotation>();
            }

            if (cameraZoom == null) {
                cameraZoom = mainCamera.gameObject.AddComponent<CameraZoom>();
            }

            cameraRotation.Init(level.Width, level.Height);
        }
    }

}