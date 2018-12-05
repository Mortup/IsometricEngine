using System;
using System.Collections.Generic;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.model.characters {

	public class Character : ICharacter {

        private List<ICharacterObserver> observers;
        protected Level level;

        private float x;
        private float y;

        public Character(Level level, int x, int y) {
            this.level = level;
            this.x = x;
            this.y = y;

            observers = new List<ICharacterObserver>();
        }

        public Level Level {
            get {
                return level;
            }
        }

        public float X {
            get {
                return x;
            }
        }

        public float Y {
            get {
                return y;
            }
        }

        public int roundedX {
            get {
                return (int)Math.Round(x);
            }
        }

        public int roundedY {
            get {
                return (int)Math.Round(y);
            }
        }

        public void Walk(float xOffset, float yOffset) {

            x += xOffset;
            y += yOffset;
            
            /**
            ITile destinationTile = level.GetTileAt(X + xOffset, Y + yOffset);
            WalkInfo walkInfo = new WalkInfo(xOffset, yOffset);

            if (destinationTile.IsWalkable(walkInfo)) {
                this.X += xOffset;
                this.Y += yOffset;

                // OnStandOver Callback
                destinationTile.OnStandOver(walkInfo);

                // OnWalk Callback
                foreach (ICharacterObserver charObs in observers) {
                    charObs.OnCharMove(xOffset, yOffset, true);
                }
            }
            else {
                // OnWalk Callback
                foreach (ICharacterObserver charObs in observers) {
                    charObs.OnCharMove(xOffset, yOffset, false);
                }
            }**/

        }

        public void Subscribe(ICharacterObserver observer) {
            observers.Add(observer);
        }

    }

}