using UnityEngine;

using com.gStudios.isometric.model.world.wall;
using com.gStudios.isometric.controller.isometricTransform;

namespace com.gStudios.isometric.controller.spriteCreators {

	public struct InmediateWallNeighbors {

        public InmediateWallNeighbors(IWall sourceWall, Orientation orientation) {

            int rotatedZ = WallTransformer.RotateCoord(new Vector3Int(0, 0, sourceWall.Z)).z;

            if (orientation == Orientation.North) {
                if (rotatedZ == 0) {
                    Top = sourceWall.GetNeighbor(-1, 0, 0);
                    TopLeft = sourceWall.GetNeighbor(0, -1, 1);
                    TopRight = sourceWall.GetNeighbor(0, 0, 1);
                    Bottom = sourceWall.GetNeighbor(1, 0, 0);
                    BottomLeft = sourceWall.GetNeighbor(1, -1, 1);
                    BottomRight = sourceWall.GetNeighbor(1, 0, 1);
                }
                else {
                    Top = sourceWall.GetNeighbor(0, -1, 1);
                    TopLeft = sourceWall.GetNeighbor(0, 0, 0);
                    TopRight = sourceWall.GetNeighbor(-1, 0, 0);
                    Bottom = sourceWall.GetNeighbor(0, 1, 1);
                    BottomLeft = sourceWall.GetNeighbor(0, 1, 0);
                    BottomRight = sourceWall.GetNeighbor(-1, 1, 0);
                }
            }
            else if (orientation == Orientation.West) {
                if (rotatedZ == 0) {
                    Top = sourceWall.GetNeighbor(0, -1, 1);
                    TopLeft = sourceWall.GetNeighbor(0, 0, 0);
                    TopRight = sourceWall.GetNeighbor(-1, 0, 0);
                    Bottom = sourceWall.GetNeighbor(0, 1, 1);
                    BottomLeft = sourceWall.GetNeighbor(0, 1, 0);
                    BottomRight = sourceWall.GetNeighbor(-1, 1, 0);
                }
                else {
                    Top = sourceWall.GetNeighbor(1, 0, 0);
                    TopLeft = sourceWall.GetNeighbor(1, 0, 1);
                    TopRight = sourceWall.GetNeighbor(1, -1, 1);
                    Bottom = sourceWall.GetNeighbor(-1, 0, 0);
                    BottomLeft = sourceWall.GetNeighbor(0, 0, 1);
                    BottomRight = sourceWall.GetNeighbor(0, -1, 1);
                }
            }
            else if (orientation == Orientation.South) {
                if (rotatedZ == 0) {
                    Top = sourceWall.GetNeighbor(1, 0, 0);
                    TopLeft = sourceWall.GetNeighbor(1, 0, 1);
                    TopRight = sourceWall.GetNeighbor(1, -1, 1);
                    Bottom = sourceWall.GetNeighbor(-1, 0, 0);
                    BottomLeft = sourceWall.GetNeighbor(0, 0, 1);
                    BottomRight = sourceWall.GetNeighbor(0, -1, 1);
                }
                else {
                    Top = sourceWall.GetNeighbor(0, 1, 1);
                    TopLeft = sourceWall.GetNeighbor(-1, 1, 0);
                    TopRight = sourceWall.GetNeighbor(0, 1, 0);
                    Bottom = sourceWall.GetNeighbor(0, -1, 1);
                    BottomLeft = sourceWall.GetNeighbor(-1, 0, 0);
                    BottomRight = sourceWall.GetNeighbor(0, 0, 0);
                }
            }
            else {
                if (rotatedZ == 0) {
                    Top = sourceWall.GetNeighbor(0, 1, 1);
                    TopLeft = sourceWall.GetNeighbor(-1, 1, 0);
                    TopRight = sourceWall.GetNeighbor(0, 1, 0);
                    Bottom = sourceWall.GetNeighbor(0, -1, 1);
                    BottomLeft = sourceWall.GetNeighbor(-1, 0, 0);
                    BottomRight = sourceWall.GetNeighbor(0, 0, 0);
                }
                else {
                    Top = sourceWall.GetNeighbor(-1, 0, 0);
                    TopLeft = sourceWall.GetNeighbor(0, -1, 1);
                    TopRight = sourceWall.GetNeighbor(0, 0, 1);
                    Bottom = sourceWall.GetNeighbor(1, 0, 0);
                    BottomLeft = sourceWall.GetNeighbor(1, -1, 1);
                    BottomRight = sourceWall.GetNeighbor(1, 0, 1);
                }
            }

        }

        public IWall Top { get; }
        public IWall TopLeft { get; private set; }
        public IWall TopRight { get; private set; }
        public IWall Bottom { get; }
        public IWall BottomLeft { get; private set; }
        public IWall BottomRight { get; private set; }

        public void FlipSideNeighbors() {
            IWall buff = TopLeft;
            TopLeft = TopRight;
            TopRight = buff;

            buff = BottomLeft;
            BottomLeft = BottomRight;
            BottomRight = buff;
        }
	}

}