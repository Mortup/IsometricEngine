using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

using com.gStudios.isometric.model.world;

public class LevelSizeTest {

	Level level;

    [SetUp]
    public void Setup() {
        level = new Level(10, 5);
    }

    [Test]
	public void LevelTestWidthStaysPasses() {
		Assert.AreEqual (level.Width, 10);
	}

	[Test]
	public void LevelTestHeightStaysPasses() {
		Assert.AreEqual (level.Height, 5);
	}
}
