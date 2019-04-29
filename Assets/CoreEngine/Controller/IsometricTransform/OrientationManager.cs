using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.model.world.orientation;

namespace com.gStudios.isometric.controller.isometricTransform {

    public static class OrientationManager {

        public static Orientation currentOrientation { get; private set; } = Orientation.North;

        private static List<IOrientationObserver> observers;

        public static void RegisterObserver(IOrientationObserver aObserver) {
            if (observers == null)
                observers = new List<IOrientationObserver>();

            if (observers.Contains(aObserver))
                Debug.LogError("Adding a observer more than once: " + aObserver.ToString());

            observers.Add(aObserver);
        }

        public static void UnregisterObserver(IOrientationObserver aObserver) {
            if (observers.Contains(aObserver) == false)
                Debug.LogError("Trying to remove a observer that's not registered.");

            observers.Remove(aObserver);
        }

        public static void RotateClockwise() {
            Orientation previousOrientation = currentOrientation;

            currentOrientation = previousOrientation.Rotate(RotationDirection.Clockwise);

            UpdateObservers(previousOrientation, currentOrientation);
        }

        public static void RotateCounterClockwise() {
            Orientation previousOrientation = currentOrientation;

            currentOrientation = previousOrientation.Rotate(RotationDirection.CounterClockwise);

            UpdateObservers(previousOrientation, currentOrientation);
        }

        public static void SetOrientation(Orientation newOrientation) {
            Orientation previousOrientation = currentOrientation;
            currentOrientation = newOrientation;
            UpdateObservers(previousOrientation, currentOrientation);
        }

        private static void UpdateObservers(Orientation previousOrientation, Orientation newOrientation) {
            if (observers == null)
                return;

            foreach(IOrientationObserver observer in observers) {
                observer.NotifyOrientationChanged(previousOrientation, newOrientation);
            }
        }
    }

}