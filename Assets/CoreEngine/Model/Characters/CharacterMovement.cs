using System;

using com.gStudios.isometric.model.world;
using com.gStudios.isometric.model.world.tile;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.model.characters {

    public class CharacterMovement {

        ICharacter character;

        private float WALL_WIDTH = 0.05f;

        public CharacterMovement(ICharacter character) {
            this.character = character;
        }

        public float[] Walk(float xOffset, float yOffset) {
            Level level = character.Level;

            float allowedXMovement = xOffset;
            float allowedYMovement = yOffset;

            float targetX = character.x + xOffset;
            float targetY = character.y + yOffset;

            WalkInfo walkInfo = new WalkInfo(xOffset, yOffset);

            int centerX = Character.ClosestCoord(targetX);
            int centerY = Character.ClosestCoord(targetY);

            int westX = Character.ClosestCoord(targetX - character.width);
            int eastX = Character.ClosestCoord(targetX + character.width);
            int northY = Character.ClosestCoord(targetY - character.height);
            int southY = Character.ClosestCoord(targetY + character.height);

            int directedX = Character.ClosestCoord(targetX + character.width * Math.Sign(xOffset));
            int directedY = Character.ClosestCoord(targetY + character.height * Math.Sign(yOffset));

            // Collide with tiles
            if (!level.GetTileAt(directedX, northY).IsWalkable(walkInfo) || !level.GetTileAt(directedX, southY).IsWalkable(walkInfo))
                allowedXMovement = 0f;

            if (!level.GetTileAt(westX, directedY).IsWalkable(walkInfo) || !level.GetTileAt(eastX, directedY).IsWalkable(walkInfo))
                allowedYMovement = 0f;

            // Collide with parallel walls
            int prevDirectedWallX = Character.ClosestCoord(character.x + character.width * Math.Sign(xOffset));
            int prevDirectedWallY = Character.ClosestCoord(character.y + character.height * Math.Sign(yOffset));

            int directedWallX = Character.ClosestCoord(targetX + (character.width+WALL_WIDTH) * Math.Sign(xOffset));
            int directedWallY = Character.ClosestCoord(targetY + (character.height+WALL_WIDTH) * Math.Sign(yOffset));

            if (directedWallX != prevDirectedWallX) {
                if (!level.GetWallBetweenTiles(prevDirectedWallX, northY, directedWallX, northY).IsEmpty())
                    allowedXMovement = 0f;
                else if (!level.GetWallBetweenTiles(prevDirectedWallX, southY, directedWallX, southY).IsEmpty())
                    allowedXMovement = 0f;
            }

            if (directedWallY != prevDirectedWallY) {
                if (!level.GetWallBetweenTiles(westX, prevDirectedWallY, westX, directedWallY).IsEmpty())
                    allowedYMovement = 0f;
                else if (!level.GetWallBetweenTiles(eastX, prevDirectedWallY, eastX, directedWallY).IsEmpty())
                    allowedYMovement = 0f;
            }

            // Collide with perpendicular walls
            int westWallX = Character.ClosestCoord(targetX - character.width - WALL_WIDTH);
            int eastWallX = Character.ClosestCoord(targetX + character.width + WALL_WIDTH);
            int northWallY = Character.ClosestCoord(targetY - character.height - WALL_WIDTH);
            int southWallY = Character.ClosestCoord(targetY + character.height + WALL_WIDTH);

            if (centerX != westWallX) {
                IWall perpendicularWall = level.GetWallBetweenTiles(centerX, directedY, westWallX, directedY);
                if (!perpendicularWall.IsEmpty())
                    allowedYMovement = 0f;
            }
            else if (centerX != eastWallX) {
                IWall perpendicularWall = level.GetWallBetweenTiles(centerX, directedY, eastWallX, directedY);
                if (!perpendicularWall.IsEmpty())
                    allowedYMovement = 0f;
            }

            if (centerY != northWallY) {
                IWall perpendicularWall = level.GetWallBetweenTiles(directedX, centerY, directedX, northWallY);
                if (!perpendicularWall.IsEmpty())
                    allowedXMovement = 0f;
            }
            else if (centerY != southWallY) {
                IWall perpendicularWall = level.GetWallBetweenTiles(directedX, centerY, directedX, southWallY);
                if (!perpendicularWall.IsEmpty())
                    allowedXMovement = 0f;
            }


            return new float[] {allowedXMovement, allowedYMovement};

            /**
            ITile destinationTile = level.GetTileAt(X + xOffset, Y + yOffset);

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
    }
}