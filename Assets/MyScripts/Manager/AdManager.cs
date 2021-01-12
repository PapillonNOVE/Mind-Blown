using EasyMobile;
using UnityEngine;

public class AdManager : Singleton<AdManager>
{
	private void Awake()
	{
		if (!RuntimeManager.IsInitialized())
		{
			RuntimeManager.Init();
		}

		// ShowBannerAds();
		//ShowInterstitalAds();
		//ShowRewardedAds();

	}

	// Subscribe to rewarded ad events
	private void OnEnable()
	{
		Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
		Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
	}

	// Unsubscribe events
	private void OnDisable()
	{
		Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
		Advertising.RewardedAdSkipped -= RewardedAdSkippedHandler;
	}

	//public void ShowBannerAds() 
	//{
	//    Advertising.ShowBannerAd(BannerAdPosition.Bottom);
	//}

	public void ShowInterstitalAds()
	{
		if (Advertising.IsInterstitialAdReady())
		{
			Advertising.ShowInterstitialAd();
		}
	}

	public void ShowRewardedAds()
	{
		if (Advertising.IsRewardedAdReady())
		{
			Advertising.ShowRewardedAd();
		}
	}

	// Event handler called when a rewarded ad has completed
	private void RewardedAdCompletedHandler(RewardedAdNetwork network, AdPlacement location)
	{
		Debug.Log("Rewarded ad has completed. The user should be rewarded now.");

		EventManager.Instance.GetQuestion?.Invoke();
	}

	// Event handler called when a rewarded ad has been skipped
	private void RewardedAdSkippedHandler(RewardedAdNetwork network, AdPlacement location)
	{
		Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");

		EventManager.Instance.GameOverTrigger?.Invoke(GameOverType.WrongAnswer);
	}
}
