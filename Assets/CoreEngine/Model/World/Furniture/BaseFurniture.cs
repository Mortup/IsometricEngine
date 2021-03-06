﻿using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.orientation;

namespace com.gStudios.isometric.model.world.furniture {

    public abstract class BaseFurniture : EmptyCallBacksFurniture {

        protected string tag = "";
        protected Level level;
        protected ITile parent;
        protected int index;
        protected Orientation orientation;

        private string _spriteVariation;
        
        public BaseFurniture(int index, Level level, ITile parent, Orientation orientation) {
            this.level = level;
            this.parent = parent;
            this.index = index;
            this.orientation = orientation;

            _spriteVariation = "";
        }

        public BaseFurniture(int index, Level level, ITile parent) : this(index, level, parent, Orientation.North) { }

        public override string GetTag() {
            return tag;
        }

        public string spriteVariation {
            get {
                return _spriteVariation;
            }

            set {
                _spriteVariation = value;
                parent.NotifyFurnitureVariationChanged();
            }
        }

        public override int GetIndex() {
            return index;
        }

        public override string GetSpriteVariation() {
            return spriteVariation;
        }

        public override Orientation GetOrientation() {
            return orientation;
        }

        public override bool IsWalkable(WalkInfo walkInfo) {
            return false;
        }

        public override void Move(int xOffset, int yOffset) {
            ITile destination = level.GetTileAt(parent.X + xOffset, parent.Y + yOffset);

            if (destination.HasFurniture() == false) {
                parent.RemoveFurniture();

                destination.PlaceFurniture(this);
                parent = destination;
            }

            OnMove(xOffset, yOffset);
        }

        public virtual void OnMove(int xOffset, int yOffset) { }

        public override sealed bool IsEmpty() {
            return true;
        }

    }

}