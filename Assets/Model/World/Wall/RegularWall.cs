using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.model.world.wall {

	public class RegularWall : AbstractWall {

		private List<IWallObserver> observers;

		public override int Type {
			get {
				return type;
			}
			set {
				type = value;

				foreach (IWallObserver wallObserver in observers) {
					wallObserver.NotifyWallTypeChanged (this);
				}
			}
		}

		public RegularWall(Level level, int x, int y, int z) : base(level, x, y, z) {
			observers = new List<IWallObserver> ();
		}

		public override void Subscribe(IWallObserver observer) {
			if (observers.Contains (observer))
				UnityEngine.Debug.LogError ("Trying to add an observer more than once.");

			observers.Add (observer);
		}
	}

}