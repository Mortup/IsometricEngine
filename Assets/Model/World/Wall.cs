using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.gStudios.isometric.model.world {

	public class Wall {

		public const int EmptyWallIndex = 0;
		public const int NewWallIndex = 1;

		int x;
		int y;
		int z;
		int type = 0;

		private List<IWallObserver> observers;

		public int Type {
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

		public int X {
			get {
				return x;
			}
		}

		public int Y {
			get {
				return y;
			}
		}

		public int Z {
			get {
				return z;
			}
		}

		public Wall(int x, int y, int z) {
			this.x = x;
			this.y = y;
			this.z = z;

			observers = new List<IWallObserver> ();
		}

		public void Subscribe(IWallObserver observer) {
			if (observers.Contains (observer))
				UnityEngine.Debug.LogError ("Trying to add an observer more than once.");

			observers.Add (observer);
		}
	}

}