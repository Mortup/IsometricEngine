namespace com.gStudios.isometric.model.world.orientation {

    public static class OrientationMath {

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

    }

}