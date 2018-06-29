using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

using com.gStudios.isometric.model.world;

public class LevelTest {

	Level level;

	[Test]
	public void LevelTestWidthStaysPasses() {
		Level level = new Level (10, 5);
		Assert.AreEqual (level.Width, 10);
	}

	[Test]
	public void LevelTestHeightStaysPasses() {
		Level level = new Level (10, 5);
		Assert.AreEqual (level.Height, 5);
	}

	[Test]
	public void LevelTestBoundsPasses() {
		Level level = new Level (10, 5);
		//level.IsInBounds
	}
}
