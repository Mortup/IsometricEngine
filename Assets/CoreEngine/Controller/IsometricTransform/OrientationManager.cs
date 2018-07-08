using System.Collections.Generic;

using UnityEngine;

namespace com.gStudios.isometric.controller.isometricTransform {

    public enum Orientation {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public static class OrientationManager {

        public static Orientation currentOrientation { get; private set; } = Orientation.East;

        private static List<IOrientationObserver> observers;

        public static void RegisterObserver(IOrientationObserver aObserver) {
            if (observers == null)
                observers = new List<IOrientationObserver>();

            if (observers.Contains(aObserver))
                Debug.LogError("Adding a observer more than once.");

            observers.Add(aObserver);
        }

        public static void UnregisterObserver(IOrientationObserver aObserver) {
            if (observers.Contains(aObserver) == false)
                Debug.LogError("Trying to remove a observer that's not registered.");

            observers.Remove(aObserver);
        }

        public static void RotateClockwise() {
            switch (currentOrientation) {
                case Orientation.North:
                    currentOrientation = Orientation.West;
                    break;
                case Orientation.West:
                    currentOrientation = Orientation.South;
                    break;
                case Orientation.South:
                    currentOrientation = Orientation.East;
                    break;
                case Orientation.East:
                    currentOrientation = Orientation.North;
                    break;
            }

            UpdateObservers();
        }

        public static void RotateCounterClockwise() {
            switch (currentOrientation) {
                case Orientation.North:
                    currentOrientation = Orientation.East;
                    break;
                case Orientation.West:
                    currentOrientation = Orientation.North;
                    break;
                case Orientation.South:
                    currentOrientation = Orientation.West;
                    break;
                case Orientation.East:
                    currentOrientation = Orientation.South;
                    break;
            }

            UpdateObservers();
        }

        private static void UpdateObservers() {
            if (observers == null)
                return;

            foreach(IOrientationObserver observer in observers) {
                observer.NotifyOrientationChanged(currentOrientation);
            }
        }
    }

}