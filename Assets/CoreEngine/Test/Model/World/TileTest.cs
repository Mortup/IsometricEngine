using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

using com.gStudios.isometric.model.world.tile;

namespace Tests.Model.World {

    public class TileTest {

        RegularTile tile;

        [SetUp]
        public void Setup() {
            tile = new RegularTile(13, 42);
        }

        [Test]
        public void TileCoordStays() {
            Assert.AreEqual(tile.X, 13);
            Assert.AreEqual(tile.Y, 42);
        }

        [Test]
        public void TileTypeInit() {
            Assert.AreEqual(tile.Type, TileIndex.Empty);
        }

        [Test]
        public void TileTypeChangeCallback() {
            int callsCount = 0;
            var observer = Substitute.For<ITileObserver>();
            observer.NotifyTileTypeChanged(Arg.Do<ITile>(x => callsCount += 1));

            Assert.AreEqual(callsCount, 0);

            tile.Type = TileIndex.New;
            Assert.AreEqual(callsCount, 0);

            tile.Subscribe(observer);

            tile.Type = TileIndex.New;
            Assert.AreEqual(callsCount, 1);

            tile.Type = TileIndex.Empty;
            tile.Type = TileIndex.New;
            Assert.AreEqual(callsCount, 3);
        }
    }

}


