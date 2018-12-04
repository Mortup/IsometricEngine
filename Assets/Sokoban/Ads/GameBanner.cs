using UnityEngine;

using GoogleMobileAds.Api;

public class GameBanner : MonoBehaviour {
    private BannerView bannerView;

    public void Start() {
        RequestBanner();
    }

    private void RequestBanner() {
        #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6795630760093369/5180491609";
        #else
        string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice("C1A5EF4590CCCEBED8C58972824C3499").Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
}
