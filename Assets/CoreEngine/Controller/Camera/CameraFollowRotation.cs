using com.gStudios.isometric.controller.isometricTransform;
using UnityEngine;

namespace com.gStudios.isometric.controller.camera {

    public class CameraFollowRotation : MonoBehaviour, IOrientationObserver {

        [SerializeField] private CoreLevelController levelController;

        private float xDiff;
        private float yDiff;

        private bool subscribedToOrientationManager = false;

        public void Init(int levelWidth, int levelHeight) {
            xDiff = ( levelHeight / 2f ) - 0.5f;
            yDiff = ( levelWidth / 4f ) - 0.25f;

            if (!subscribedToOrientationManager) {
                OrientationManager.RegisterObserver(this);
            }
        }

        public void NotifyOrientationChanged(Orientation previousOrientation, Orientation newOrientation) {
            RotationDirection dir = OrientationManager.GetDirection(previousOrientation, newOrientation);
            if (dir == RotationDirection.Clockwise) {
                RotateClockwise(previousOrientation);
            }
            else if (dir == RotationDirection.CounterClockwise) {
                RotateCounterClockwise(previousOrientation);
            }
            else {
                Debug.LogError("Unsupported rotation");
            }
        }

        private void RotateClockwise(Orientation startingOrientation) {
            Vector3 rotation = Vector3.zero;
            Debug.Log(startingOrientation);
            switch (startingOrientation) {
                case Orientation.North:
                    rotation.x = -xDiff;
                    rotation.y = yDiff;
                    break;
                case Orientation.West:
                    rotation.x = xDiff;
                    rotation.y = yDiff;
                    break;
                case Orientation.South:
                    rotation.x = xDiff;
                    rotation.y = -yDiff;
                    break;
                case Orientation.East:
                    rotation.x = -xDiff;
                    rotation.y = -yDiff;
                    break;
            }

            transform.position += rotation;
        }

        private void RotateCounterClockwise(Orientation startingOrientation) {
            Vector3 rotation = Vector3.zero;
            Debug.Log(startingOrientation);
            switch (startingOrientation) {
                case Orientation.South:
                    rotation.x = -xDiff;
                    rotation.y = -yDiff;
                    break;
                case Orientation.West:
                    rotation.x = xDiff;
                    rotation.y = -yDiff;
                    break;
                case Orientation.North:
                    rotation.x = xDiff;
                    rotation.y = yDiff;
                    break;
                case Orientation.East:
                    rotation.x = -xDiff;
                    rotation.y = yDiff;
                    break;
            }

            transform.position += rotation;
        }
    }

}