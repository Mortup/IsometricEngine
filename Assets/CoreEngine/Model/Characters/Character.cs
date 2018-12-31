using System;
using System.Collections.Generic;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.model.characters {

	public class Character : ICharacter {

        private CharacterMovement charMovement;

        private List<ICharacterObserver> observers;
        protected Level level;

        public Character(Level level, int x, int y) {
            this.level = level;
            this.x = x;
            this.y = y;

            charMovement = new CharacterMovement(this);
            observers = new List<ICharacterObserver>();

            width = 0.25f;
            height = 0.3f;
        }

        public Level Level {
            get {
                return level;
            }
        }

        public float x { get; private set; }
        public float y { get; private set; }

        public int roundedX { get { return (int)Math.Round(x); } }

        public int roundedY { get { return (int)Math.Round(y); } }

        public float width { get; private set; }
        public float height { get; private set; }

        public int roundedWestX { get { return (int)Math.Round(x - width); } }
        public int roundedEastX { get { return (int)Math.Round(x + width); } }
        public int roundedNorthY { get { return (int)Math.Round(y - height); } }
        public int roundedSouthY { get { return (int)Math.Round(y + height); } }


        public void Walk(float xOffset, float yOffset) {
            float[] allowedOffset = charMovement.Walk(xOffset, yOffset);

            x += allowedOffset[0];
            y += allowedOffset[1];
        }

        public void Subscribe(ICharacterObserver observer) {
            observers.Add(observer);
        }

        public static int ClosestCoord(float c) {
            return (int)Math.Round(c);
        }

    }

}