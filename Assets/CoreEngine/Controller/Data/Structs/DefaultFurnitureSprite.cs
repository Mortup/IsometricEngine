using UnityEngine;

using com.gStudios.isometric.model.world.orientation;
using com.gStudios.isometric.model.world.furniture;
using com.gStudios.isometric.controller.isometricTransform;

namespace com.gStudios.isometric.controller.data.structs {

    public class DefaultFurnitureSprite : IFurnitureSprite {

        Sprite north, south, east, west;

        public DefaultFurnitureSprite(Sprite[] spritePack) {
            if (spritePack.Length != 4) {
                Debug.LogError("Trying to create a default furniture sprite without 4 sprites");
            }

            foreach (Sprite spr in spritePack) {
                string orientation = spr.name.Split('_')[1];
                switch (orientation) {
                    case "n":
                        north = spr;
                        break;
                    case "s":
                        south = spr;
                        break;
                    case "e":
                        east = spr;
                        break;
                    case "w":
                        west = spr;
                        break;
                    default:
                        Debug.LogError("Unrecognized sprite nomenclature");
                        break;
                }
            }
        }

        public Sprite GetSprite(IFurniture furniture) {
            return GetSprite(furniture.GetOrientation());
        }

        public Sprite GetSprite(Orientation orientation) {
            Orientation currentOrientation = OrientationManager.currentOrientation;
            RotationDirection rotation = RotationDirection.NoRotation;

            switch(orientation) {
                case Orientation.North:
                    rotation = RotationDirection.NoRotation;
                    break;
                case Orientation.South:
                    rotation = RotationDirection.HalfRotation;
                    break;
                case Orientation.East:
                    rotation = RotationDirection.CounterClockwise;
                    break;
                case Orientation.West:
                    rotation = RotationDirection.Clockwise;
                    break;
                default:
                    Debug.LogError("Unknown rotation");
                    break;
            }

            Orientation rotatedOrientation = currentOrientation.GetRotated(rotation);

            switch (rotatedOrientation) {
                case Orientation.North:
                    return north;
                case Orientation.South:
                    return south;
                case Orientation.East:
                    return east;
                default:
                    return west;
            }
        }

        public Sprite GetThumbnail() {
            return north;
        }
    }

}