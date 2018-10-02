using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world;

public class SokobanCharMovement : MonoBehaviour {

    private bool initializated = false;

    private Stack<Vector2Int> previousPositions;
    private Stack<bool> previousPushes;

    private ICharacter character;
    private Level level;

    public void Init(ICharacter character, Level level) {
        this.character = character;
        this.level = level;

        previousPositions = new Stack<Vector2Int>();
        previousPositions.Push(CurrentPos());

        initializated = true;
    }

    public void UpdatePosition() {
        gameObject.transform.position = TileTransformer.CoordToWorld(character.X, character.Y);
    }

    public void Update() {
        if (!initializated)
            Debug.LogError("Character controllers must be initializated inmediately!");

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            character.Walk(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            character.Walk(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            character.Walk(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            character.Walk(0, 1);
        }

        UpdatePosition();

        if (CurrentPos() != previousPositions.Peek()) {
            previousPositions.Push(CurrentPos());
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            UndoLastMovement();
        }
    }

    private void UndoLastMovement() {
        if (previousPositions.Count > 1) {
            previousPositions.Pop();

            Vector2Int previousPosition = previousPositions.Peek();
            Vector2Int inverseMovement = new Vector2Int(character.X - previousPosition.x, character.Y - previousPosition.y);
            Vector2Int pushedBoxPos = new Vector2Int(inverseMovement.x + character.X, inverseMovement.y + character.Y);

            Vector2Int movement = new Vector2Int(previousPosition.x - character.X, previousPosition.y - character.Y);

            character.Walk(movement.x, movement.y);
            level.GetTileAt(pushedBoxPos.x, pushedBoxPos.y).GetPlacedFurniture().Move(movement.x, movement.y);



        }
    }

    private Vector2Int CurrentPos() {
        return new Vector2Int(character.X, character.Y);
    }

}
