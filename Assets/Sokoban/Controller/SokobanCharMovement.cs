using System.Collections.Generic;

using UnityEngine;

using com.gStudios.isometric.controller.characters;
using com.gStudios.isometric.model.characters;
using com.gStudios.isometric.model.world;

public class SokobanCharMovement : SimpleMovementCC {

    private Stack<Vector2Int> previousPositions;

    public override void Init(ICharacter character) {
        base.Init(character);

        previousPositions = new Stack<Vector2Int>();
        previousPositions.Push(CurrentPos());
    }

    public override void Update() {
        base.Update();

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
            character.Walk(previousPosition.x - character.X, previousPosition.y - character.Y);
            
        }
    }

    private Vector2Int CurrentPos() {
        return new Vector2Int(character.X, character.Y);
    }

}
