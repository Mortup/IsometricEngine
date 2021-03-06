﻿using com.gStudios.isometric.model.world.orientation;

namespace com.gStudios.isometric.controller.isometricTransform {

	public interface IOrientationObserver {

        void NotifyOrientationChanged(Orientation previousOrientation, Orientation newOrientation);	
		
	}

}