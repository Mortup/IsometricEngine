using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

using com.gStudios.isometric.model.world;

public class LevelInBoundsTest {

    Level level;

    [SetUp]
    public void Setup() {
        level = new Level(10, 10);
    }

    [Test]
    public void TileInsideCoordPasses() {
        Assert.IsTrue(level.IsTileInBounds(5, 3));
    }

    [Test]
    public void TileOutsideCoordPasses() {
        Assert.IsFalse(level.IsTileInBounds(15, 15));
    }

    [Test]
    public void TileInsideBorderCoordPasses() {
        Assert.IsTrue(level.IsTileInBounds(0, 0));
        Assert.IsTrue(level.IsTileInBounds(9, 9));
    }

    [Test]
    public void TileOutsideBorderCoordPasses() {
        Assert.IsFalse(level.IsTileInBounds(-1, 5));
        Assert.IsFalse(level.IsTileInBounds(10, 5));
        Assert.IsFalse(level.IsTileInBounds(5, -1));
        Assert.IsFalse(level.IsTileInBounds(5, 10));
    }

    [Test]
    public void WallInsideCoordPasses() {
        Assert.IsTrue(level.IsWallInBounds(5, 5, 0));
    }

    [Test]
    public void WallOusideCoordPasses() {
        Assert.IsFalse(level.IsWallInBounds(15, 15, 3));
    }

    [Test]
    public void WallOutsideZPasses() {
        Assert.IsFalse(level.IsWallInBounds(0, 0, 2));
    }

    [Test]
    public void WallInsideBorderCoordPasses() {
        Assert.IsTrue(level.IsWallInBounds(0, 0, 0));
        Assert.IsTrue(level.IsWallInBounds(10, 10, 1));
    }

    [Test]
    public void WallOutsideBorderCoordPasses() {
        Assert.IsFalse(level.IsWallInBounds(-1, 5, 0));
        Assert.IsFalse(level.IsWallInBounds(11, 5, 1));
        Assert.IsFalse(level.IsWallInBounds(5, -1, 1));
        Assert.IsFalse(level.IsWallInBounds(5, 11, 0));
    }
}
