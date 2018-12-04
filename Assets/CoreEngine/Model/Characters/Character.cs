﻿using System.Collections.Generic;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.model.characters {

	public class Character : ICharacter {

        private List<ICharacterObserver> observers;
        protected Level level;

        public Character(Level level, int x, int y) {
            this.level = level;
            this.X = x;
            this.Y = y;

            observers = new List<ICharacterObserver>();
        }

        public int X { get; protected set; }

        public int Y { get; protected set; }

        public void Walk(int xOffset, int yOffset) {
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
            }

        }

        public void Subscribe(ICharacterObserver observer) {
            observers.Add(observer);
        }

    }

}