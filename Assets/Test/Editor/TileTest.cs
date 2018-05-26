using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

using com.gStudios.isometric.model.world.tile;

public class TileTest {

	RegularTile tile;

	[SetUp]
	public void Setup() {
		tile = new RegularTile (13, 42);
	}

	[Test]
	public void TileCoordStays() {
		Assert.AreEqual (tile.X, 13);
		Assert.AreEqual (tile.Y, 42);
	}

	[Test]
	public void TileTypeInit() {
		Assert.AreEqual (tile.Type, TileIndex.EmptyTileIndex);
	}

	[Test]
	public void TileTypeChangeCallback() {
		RegularTile tile = new RegularTile (13, 42);

		int callsCount = 0;
        var observer = Substitute.For<ITileObserver>();
        observer.NotifyTileTypeChanged(Arg.Do<ITile>(x => callsCount += 1));

        Assert.AreEqual(callsCount, 0);

        tile.Type = TileIndex.NewTileIndex;
        Assert.AreEqual(callsCount, 0);

        tile.Subscribe(observer);

        tile.Type = TileIndex.NewTileIndex;
        Assert.AreEqual(callsCount, 1);

        tile.Type = TileIndex.EmptyTileIndex;
        tile.Type = TileIndex.NewTileIndex;
        Assert.AreEqual(callsCount, 3);
    }
}
