namespace com.gStudios.isometric.model.world.orientation {

    public enum Orientation {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }   

    public static class OrientationMethods {

        public static Orientation Rotate(this Orientation orientation, RotationDirection direction) {

            switch (direction) {
                case RotationDirection.Clockwise:
                    switch (orientation) {
                        case Orientation.North:
                            return Orientation.West;
                        case Orientation.West:
                            return Orientation.South;
                        case Orientation.South:
                            return Orientation.East;
                        case Orientation.East:
                            return Orientation.North;
                        default:
                            UnityEngine.Debug.LogError("Unknown orientation");
                            return Orientation.North;
                    }
                case RotationDirection.CounterClockwise:
                    switch (orientation) {
                        case Orientation.North:
                            return Orientation.East;
                        case Orientation.West:
                            return Orientation.North;
                        case Orientation.South:
                            return Orientation.West;
                        case Orientation.East:
                            return Orientation.South;
                        default:
                            UnityEngine.Debug.LogError("Unknown orientation");
                            return Orientation.North;
                    }
                case RotationDirection.HalfRotation:
                    switch(orientation) {
                        case Orientation.North:
                            return Orientation.South;
                        case Orientation.West:
                            return Orientation.East;
                        case Orientation.South:
                            return Orientation.North;
                        case Orientation.East:
                            return Orientation.West;
                        default:
                            UnityEngine.Debug.LogError("Unknown orientation");
                            return Orientation.North;
                    }
                case RotationDirection.NoRotation:
                    return orientation;
                default:
                    UnityEngine.Debug.LogError("Unknown rotation direction");
                    return orientation;
            }

        }

    }

}