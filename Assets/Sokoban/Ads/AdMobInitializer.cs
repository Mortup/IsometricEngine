using UnityEngine;

using GoogleMobileAds.Api;

public class AdMobInitializer : MonoBehaviour {

	void Start () {
		#if UNITY_ANDROID
            string appId = "ca-app-pub-3940256099942544~3347511713";
        #else
            string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
	}

}
