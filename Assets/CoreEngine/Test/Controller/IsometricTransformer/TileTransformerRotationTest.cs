﻿using NUnit.Framework;
using UnityEngine;

using com.gStudios.isometric.model.world.orientation;
using com.gStudios.isometric.controller.isometricTransform;

namespace Tests.Controller.IsometricTransformer
{
    public class TileTransformerRotationTest
    {
        [SetUp]
        public void Setup() {
            OrientationManager.SetOrientation(Orientation.North);
        }

        [Test]
        public void NorthRotationIsIdentity() {
            OrientationManager.SetOrientation(Orientation.North);

            Vector2Int coord1 = new Vector2Int(73, 12);
            Vector2Int coord2 = new Vector2Int(-89, 22);
            Vector2Int coord3 = new Vector2Int(10, -4);

            Assert.AreEqual(coord1, TileTransformer.RotateCoord(coord1));
            Assert.AreEqual(coord2, TileTransformer.RotateCoord(coord2));
            Assert.AreEqual(coord3, TileTransformer.RotateCoord(coord3));

            Assert.AreEqual(coord1, TileTransformer.InverseRotateCoord(coord1));
            Assert.AreEqual(coord2, TileTransformer.InverseRotateCoord(coord2));
            Assert.AreEqual(coord3, TileTransformer.InverseRotateCoord(coord3));
        }

        [Test]
        public void FloatRotationsKeepDecimals() {
            Vector2 coord1 = new Vector2(39.4f, 12.2f);

            Assert.AreEqual(TileTransformer.RotateCoord(coord1), new Vector2(39.4f, 12.2f));
            Assert.AreEqual(TileTransformer.InverseRotateCoord(coord1), new Vector2(39.4f, 12.2f));

            OrientationManager.SetOrientation(Orientation.West);
            Assert.AreEqual(TileTransformer.RotateCoord(coord1), new Vector2(12.2f, -39.4f));
            Assert.AreEqual(TileTransformer.InverseRotateCoord(coord1), new Vector2(-12.2f, 39.4f));
        }

        [Test]
        public void RegularRotationBehaviour() {
            Vector2Int coord = new Vector2Int(34, -21);

            OrientationManager.SetOrientation(Orientation.East);
            Assert.AreEqual(TileTransformer.RotateCoord(coord), new Vector2Int(21, 34));

            OrientationManager.SetOrientation(Orientation.West);
            Assert.AreEqual(TileTransformer.RotateCoord(coord), new Vector2Int(-21, -34));

            OrientationManager.SetOrientation(Orientation.South);
            Assert.AreEqual(TileTransformer.RotateCoord(coord), new Vector2Int(-34, 21));
        }

        [Test]
        public void InverseRotationBehaviour() {
            Vector2Int coord = new Vector2Int(-45, -23);

            OrientationManager.SetOrientation(Orientation.East);
            Assert.AreEqual(TileTransformer.InverseRotateCoord(coord), new Vector2Int(-23, 45));

            OrientationManager.SetOrientation(Orientation.West);
            Assert.AreEqual(TileTransformer.InverseRotateCoord(coord), new Vector2Int(23, -45));

            OrientationManager.SetOrientation(Orientation.South);
            Assert.AreEqual(TileTransformer.InverseRotateCoord(coord), new Vector2Int(45, 23));
        }

        [Test]
        public void TileRotationIsInvertible()
        {
            Vector2Int coord1 = new Vector2Int(13, 32);
            Vector2Int coord2 = new Vector2Int(-42, 78);
            Vector2Int coord3 = new Vector2Int(-81, -70);

            // North
            OrientationManager.SetOrientation(Orientation.North);

            Vector2Int rotated1 = TileTransformer.RotateCoord(coord1);
            Vector2Int rotated2 = TileTransformer.RotateCoord(coord2);
            Vector2Int rotated3 = TileTransformer.RotateCoord(coord3);

            Vector2Int inverseRotated1 = TileTransformer.InverseRotateCoord(coord1);
            Vector2Int inverseRotated2 = TileTransformer.InverseRotateCoord(coord2);
            Vector2Int inverseRotated3 = TileTransformer.InverseRotateCoord(coord3);

            Assert.AreEqual(coord1, TileTransformer.InverseRotateCoord(rotated1));
            Assert.AreEqual(coord2, TileTransformer.InverseRotateCoord(rotated2));
            Assert.AreEqual(coord3, TileTransformer.InverseRotateCoord(rotated3));

            Assert.AreEqual(coord1, TileTransformer.RotateCoord(inverseRotated1));
            Assert.AreEqual(coord2, TileTransformer.RotateCoord(inverseRotated2));
            Assert.AreEqual(coord3, TileTransformer.RotateCoord(inverseRotated3));

            // East
            OrientationManager.SetOrientation(Orientation.East);

            rotated1 = TileTransformer.RotateCoord(coord1);
            rotated2 = TileTransformer.RotateCoord(coord2);
            rotated3 = TileTransformer.RotateCoord(coord3);

            inverseRotated1 = TileTransformer.InverseRotateCoord(coord1);
            inverseRotated2 = TileTransformer.InverseRotateCoord(coord2);
            inverseRotated3 = TileTransformer.InverseRotateCoord(coord3);

            Assert.AreEqual(coord1, TileTransformer.InverseRotateCoord(rotated1));
            Assert.AreEqual(coord2, TileTransformer.InverseRotateCoord(rotated2));
            Assert.AreEqual(coord3, TileTransformer.InverseRotateCoord(rotated3));

            Assert.AreEqual(coord1, TileTransformer.RotateCoord(inverseRotated1));
            Assert.AreEqual(coord2, TileTransformer.RotateCoord(inverseRotated2));
            Assert.AreEqual(coord3, TileTransformer.RotateCoord(inverseRotated3));

            // West
            OrientationManager.SetOrientation(Orientation.West);

            rotated1 = TileTransformer.RotateCoord(coord1);
            rotated2 = TileTransformer.RotateCoord(coord2);
            rotated3 = TileTransformer.RotateCoord(coord3);

            inverseRotated1 = TileTransformer.InverseRotateCoord(coord1);
            inverseRotated2 = TileTransformer.InverseRotateCoord(coord2);
            inverseRotated3 = TileTransformer.InverseRotateCoord(coord3);

            Assert.AreEqual(coord1, TileTransformer.InverseRotateCoord(rotated1));
            Assert.AreEqual(coord2, TileTransformer.InverseRotateCoord(rotated2));
            Assert.AreEqual(coord3, TileTransformer.InverseRotateCoord(rotated3));

            Assert.AreEqual(coord1, TileTransformer.RotateCoord(inverseRotated1));
            Assert.AreEqual(coord2, TileTransformer.RotateCoord(inverseRotated2));
            Assert.AreEqual(coord3, TileTransformer.RotateCoord(inverseRotated3));

            // South
            OrientationManager.SetOrientation(Orientation.South);

            rotated1 = TileTransformer.RotateCoord(coord1);
            rotated2 = TileTransformer.RotateCoord(coord2);
            rotated3 = TileTransformer.RotateCoord(coord3);

            inverseRotated1 = TileTransformer.InverseRotateCoord(coord1);
            inverseRotated2 = TileTransformer.InverseRotateCoord(coord2);
            inverseRotated3 = TileTransformer.InverseRotateCoord(coord3);

            Assert.AreEqual(coord1, TileTransformer.InverseRotateCoord(rotated1));
            Assert.AreEqual(coord2, TileTransformer.InverseRotateCoord(rotated2));
            Assert.AreEqual(coord3, TileTransformer.InverseRotateCoord(rotated3));

            Assert.AreEqual(coord1, TileTransformer.RotateCoord(inverseRotated1));
            Assert.AreEqual(coord2, TileTransformer.RotateCoord(inverseRotated2));
            Assert.AreEqual(coord3, TileTransformer.RotateCoord(inverseRotated3));
        }
        
    }
}
