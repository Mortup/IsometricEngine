using UnityEngine;

using com.gStudios.isometric.model.world;

namespace com.gStudios.isometric.controller.camera {

    public class CameraController : MonoBehaviour, ILevelController {

        [SerializeField] private bool addCameraDrag;
        [SerializeField] private Camera mainCamera;

        private CameraDrag cameraDrag;
        private CameraFollowRotation cameraRotation;
        private CameraZoom cameraZoom;

        public void Init(CoreLevelController clc) {
        }

        public void OnLevelInit(Level level) {

            if (cameraDrag == null && addCameraDrag) {
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