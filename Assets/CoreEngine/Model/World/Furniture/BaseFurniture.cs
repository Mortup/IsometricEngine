using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world.tile;

namespace com.gStudios.isometric.model.world.furniture {

    public abstract class BaseFurniture : EmptyCallBacksFurniture {

        protected string tag = "";
        protected Level level;
        protected ITile parent;

        private string _spriteVariation;

        public BaseFurniture(Level level, ITile parent) {
            this.level = level;
            this.parent = parent;

            _spriteVariation = "";
        }

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


        public override string GetSpriteVariation() {
            return spriteVariation;
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

        public override sealed bool IsFurniture() {
            return true;
        }

    }

}