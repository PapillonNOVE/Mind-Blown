using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class AppodealAdManager : MonoBehaviour, IRewardedVideoAdListener
{
	int timesTriedToShowInterstitial = 0;

	void Start()
	{
		Init();	
	}

	// Use this for initialization
	private void Init()
	{
		string appKey = "Put your app key here.";
		Appodeal.disableLocationPermissionCheck();
		Appodeal.setTesting(true);
		Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.BANNER | Appodeal.REWARDED_VIDEO);
		Appodeal.setRewardedVideoCallbacks(this);
	}

	#region Banner

	public void ShowBanner()
    {
        if (Appodeal.isLoaded(Appodeal.BANNER))
        {
            Appodeal.show(Appodeal.BANNER_TOP);
        }
    }

    public void HideBanner()
    {
        Appodeal.hide(Appodeal.BANNER);
    }

	#endregion

	#region Interstitial

	public void ShowInterstitial()
    {
        timesTriedToShowInterstitial++;

        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL) && timesTriedToShowInterstitial >= 5)
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
            timesTriedToShowInterstitial = 0;
        }
    }

	#endregion

	#region Rewarded

	public void ShowRewarded()
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
    }

	public void onRewardedVideoLoaded(bool precache)
	{
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoShowFailed()
	{
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoFinished(double amount, string name)
	{
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoClosed(bool finished)
	{
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoExpired()
	{
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoClicked()
	{
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoFailedToLoad()
	{
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoShown()
	{
		throw new System.NotImplementedException();
	}

	#endregion

}
