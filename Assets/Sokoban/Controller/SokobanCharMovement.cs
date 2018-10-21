using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world;

namespace com.gStudios.sokoban.controller.characters {

    public class SokobanCharMovement : MonoBehaviour {

        private bool initializated = false;

        private Stack<Vector2Int> previousPositions;
        private Stack<bool> previousPushes;

        private ICharacter character;
        private Level level;
        private SokobanCharSprites scs;

        public void Init(ICharacter character, Level level, SokobanCharSprites scs) {
            this.character = character;
            this.level = level;
            this.scs = scs;

            previousPositions = new Stack<Vector2Int>();
            previousPushes = new Stack<bool>();
            previousPositions.Push(CurrentPos());

            initializated = true;
        }

        public void UpdatePosition() {
            gameObject.transform.position = TileTransformer.CoordToWorld(character.X, character.Y);
        }

        public void Update() {
            if (!initializated)
                Debug.LogError("Character controllers must be initializated inmediately!");

            UpdatePosition();
        }

        public void Move(string direction) {
            if (!initializated)
                Debug.LogError("Character controllers must be initializated inmediately!");

            bool pushedSomething = false;

            int xOff = 0;
            int yOff = 0;

            if (direction == "up") {
                xOff = -1;
            }
            else if (direction == "down") {
                xOff = 1;
            }
            else if (direction == "left") {
                yOff = -1;
            }
            else if (direction == "right") {
                yOff = 1;
            }
            else {
                Debug.LogError("Unknown walking direction.");
            }

            if (level.GetTileAt(character.X + xOff, character.Y + yOff).HasFurniture())
                pushedSomething = true;

            character.Walk(xOff, yOff);

            scs.UpdateSprite(xOff, yOff, false);
            UpdatePosition();

            if (CurrentPos() != previousPositions.Peek()) {
                previousPositions.Push(CurrentPos());
                previousPushes.Push(pushedSomething);
            }
        }

        public void Undo() {
            UndoLastMovement();
        }

        private void UndoLastMovement() {
            if (previousPositions.Count > 1) {
                previousPositions.Pop();


                Vector2Int previousPosition = previousPositions.Peek();
                Vector2Int inverseMovement = new Vector2Int(character.X - previousPosition.x, character.Y - previousPosition.y);
                Vector2Int pushedBoxPos = new Vector2Int(inverseMovement.x + character.X, inverseMovement.y + character.Y);

                Vector2Int movement = new Vector2Int(previousPosition.x - character.X, previousPosition.y - character.Y);

                character.Walk(movement.x, movement.y);
                scs.UpdateSprite(movement.x, movement.y, true);

                if (previousPushes.Pop())
                    level.GetTileAt(pushedBoxPos.x, pushedBoxPos.y).GetPlacedFurniture().Move(movement.x, movement.y);

            }
        }

        private Vector2Int CurrentPos() {
            return new Vector2Int(character.X, character.Y);
        }
    }
}

