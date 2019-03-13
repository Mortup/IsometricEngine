using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using com.gStudios.isometric.controller.isometricTransform;

namespace Tests.Controller.IsometricTransformer
{
    public class TileTransformerTest
    {
        [SetUp]
        public void Setup() {
            OrientationManager.SetOrientation(Orientation.North);
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
