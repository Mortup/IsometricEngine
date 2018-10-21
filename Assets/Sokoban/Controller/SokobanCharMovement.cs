﻿using System.Collections.Generic;

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

        bool pushedSomething = false;

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (level.GetTileAt(character.X + 1, character.Y).HasFurniture())
                pushedSomething = true;
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (level.GetTileAt(character.X, character.Y - 1).HasFurniture())
                pushedSomething = true;
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (level.GetTileAt(character.X, character.Y + 1).HasFurniture())
                pushedSomething = true;
            
        }

        UpdatePosition();

        if (CurrentPos() != previousPositions.Peek()) {
            previousPositions.Push(CurrentPos());
            previousPushes.Push(pushedSomething);
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            UndoLastMovement();
        }
    }

    public void MoveUp() {
        character.Walk(-1, 0);
    }

    public void MoveDown() {
        character.Walk(1, 0);
    }

    public void MoveLeft() {
        character.Walk(0, -1);
    }

    public void MoveRight() {
        character.Walk(0, 1);
    }

    private void UndoLastMovement() {
        if (previousPositions.Count > 1) {
            previousPositions.Pop();
            

            Vector2Int previousPosition = previousPositions.Peek();
            Vector2Int inverseMovement = new Vector2Int(character.X - previousPosition.x, character.Y - previousPosition.y);
            Vector2Int pushedBoxPos = new Vector2Int(inverseMovement.x + character.X, inverseMovement.y + character.Y);

            Vector2Int movement = new Vector2Int(previousPosition.x - character.X, previousPosition.y - character.Y);

            character.Walk(movement.x, movement.y);

            if (previousPushes.Pop())
                level.GetTileAt(pushedBoxPos.x, pushedBoxPos.y).GetPlacedFurniture().Move(movement.x, movement.y);



        }
    }

    private Vector2Int CurrentPos() {
        return new Vector2Int(character.X, character.Y);
    }

}
