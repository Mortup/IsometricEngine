using System;

namespace com.gStudios.isometric.model.characters {

	public struct WalkInfo {

        public float xDirection;
        public float yDirection;

        public WalkInfo(float xDirection, float yDirection) {
            this.xDirection = xDirection;
            this.yDirection = yDirection;
        }
	}
	
}