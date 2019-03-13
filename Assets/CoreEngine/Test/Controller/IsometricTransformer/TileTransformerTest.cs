using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.isometricTransform;

namespace Tests.Controller.IsometricTransformer
{
    public class TileTransformerTest {

        [Test]
        public void CoordToWorldMapping() {
            OrientationManager.SetOrientation(Orientation.North);

            Vector2Int coord1 = new Vector2Int(74, -23);
            Vector2Int coord2 = new Vector2Int(-33, 13);
            Vector2Int coord3 = new Vector2Int(93, 0);

            Vector2 expectedWorldPos1 = new Vector2(
                -98 * Settings.TILE_WIDTH_HALF,
                -51 * Settings.TILE_HEIGHT_HALF);
            Assert.AreEqual(TileTransformer.CoordToWorld(coord1), expectedWorldPos1);
            Assert.AreEqual(TileTransformer.CoordToWorld(coord1.x, coord1.y), expectedWorldPos1);
            Assert.AreEqual(TileTransformer.CoordToWorld((float)coord1.x, (float)coord1.y), expectedWorldPos1);

            Vector2 expectedWorldPos2 = new Vector2(
                45 * Settings.TILE_WIDTH_HALF,
                20 * Settings.TILE_HEIGHT_HALF);
            Assert.AreEqual(TileTransformer.CoordToWorld(coord2), expectedWorldPos2);
            Assert.AreEqual(TileTransformer.CoordToWorld(coord2.x, coord2.y), expectedWorldPos2);
            Assert.AreEqual(TileTransformer.CoordToWorld((float)coord2.x, (float)coord2.y), expectedWorldPos2);

            Vector2 expectedWorldPos3 = new Vector2(
                -94 * Settings.TILE_WIDTH_HALF,
                -93 * Settings.TILE_HEIGHT_HALF);
            Assert.AreEqual(TileTransformer.CoordToWorld(coord3), expectedWorldPos3);
            Assert.AreEqual(TileTransformer.CoordToWorld(coord3.x, coord3.y), expectedWorldPos3);
            Assert.AreEqual(TileTransformer.CoordToWorld((float)coord3.x, (float)coord3.y), expectedWorldPos3);
        }

        [Test]
        public void CoordToWorldRotates() {
            OrientationManager.SetOrientation(Orientation.North);
            Vector2Int originalCoord = new Vector2Int(77, 23);
            Vector2 originalWorld = TileTransformer.CoordToWorld(originalCoord);

            OrientationManager.SetOrientation(Orientation.East);
            Vector2Int eastCoord = TileTransformer.RotateCoord(originalCoord);
            Vector2 eastWorldFromOriginal = TileTransformer.CoordToWorld(originalCoord);

            OrientationManager.SetOrientation(Orientation.West);
            Vector2Int westCoord = TileTransformer.RotateCoord(originalCoord);
            Vector2 westWorldFromOriginal = TileTransformer.CoordToWorld(originalCoord);
            
            OrientationManager.SetOrientation(Orientation.South);
            Vector2Int southCoord = TileTransformer.RotateCoord(originalCoord);
            Vector2 southWorldFromOriginal = TileTransformer.CoordToWorld(originalCoord);

            OrientationManager.SetOrientation(Orientation.North);

            Assert.AreEqual(TileTransformer.CoordToWorld(eastCoord), eastWorldFromOriginal);
            Assert.AreEqual(TileTransformer.CoordToWorld(westCoord), westWorldFromOriginal);
            Assert.AreEqual(TileTransformer.CoordToWorld(southCoord), southWorldFromOriginal);
        }

        [Test]
        public void WorldToCoordRounds() {
            OrientationManager.SetOrientation(Orientation.North);

            Vector2Int coords1 = new Vector2Int(42, 0);
            Vector2Int coords2 = new Vector2Int(-11, -63);
            Vector2Int coords3 = new Vector2Int(-52, 53);

            Vector2 world1 = TileTransformer.CoordToWorld(coords1) +
                new Vector2(Settings.TILE_WIDTH / 6f, Settings.TILE_HEIGHT / 9f);

            Vector2 world2 = TileTransformer.CoordToWorld(coords2) +
                new Vector2(Settings.TILE_WIDTH / 3f, 0f);

            // This test adds a big offset so the coords change.
            Vector2 world3 = TileTransformer.CoordToWorld(coords3) + 
                new Vector2(Settings.TILE_WIDTH_HALF, Settings.TILE_HEIGHT * 0.9f);

            Assert.AreEqual(coords1, TileTransformer.WorldToCoord(world1));
            Assert.AreEqual(coords2, TileTransformer.WorldToCoord(world2));
            Assert.AreEqual(coords3, TileTransformer.WorldToCoord(world3) + Vector2Int.one);
        }
    }
}
