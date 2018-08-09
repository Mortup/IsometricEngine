using System.Collections.Generic;

using UnityEngine;

namespace com.gStudios.isometric.controller.isometricTransform {

    public enum Orientation {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public enum RotationDirection {
        Clockwise = 0,
        CounterClockwise = 1,
        HalfRotation = 2,
        NoRotation = 3
    }

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

            UpdateObservers(previousOrientation, currentOrientation);
        }

        public static void RotateCounterClockwise() {
            Orientation previousOrientation = currentOrientation;

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

            UpdateObservers(previousOrientation, currentOrientation);
        }

        public static RotationDirection GetDirection(Orientation start, Orientation end) {
            switch (start) {
                case Orientation.North:
                    switch (end) {
                        case Orientation.West:
                            return RotationDirection.Clockwise;
                        case Orientation.East:
                            return RotationDirection.CounterClockwise;
                        case Orientation.South:
                            return RotationDirection.HalfRotation;
                    }
                    break;
                case Orientation.West:
                    switch (end) {
                        case Orientation.South:
                            return RotationDirection.Clockwise;
                        case Orientation.North:
                            return RotationDirection.CounterClockwise;
                        case Orientation.West:
                            return RotationDirection.HalfRotation;
                    }
                    break;
                case Orientation.South:
                    switch (end) {
                        case Orientation.East:
                            return RotationDirection.Clockwise;
                        case Orientation.West:
                            return RotationDirection.CounterClockwise;
                        case Orientation.South:
                            return RotationDirection.HalfRotation;
                    }
                    break;
                case Orientation.East:
                    switch (end) {
                        case Orientation.North:
                            return RotationDirection.Clockwise;
                        case Orientation.South:
                            return RotationDirection.CounterClockwise;
                        case Orientation.East:
                            return RotationDirection.HalfRotation;
                    }
                    break;
            }
            return RotationDirection.NoRotation;
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