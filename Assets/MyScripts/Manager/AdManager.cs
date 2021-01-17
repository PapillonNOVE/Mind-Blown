using EasyMobile;
using System.Collections;
using UnityEngine;

public class AdManager : Singleton<AdManager>
{
	private WaitUntil m_WaitForInterstitialAd;
	private WaitUntil m_WaitForRewardedAd;

	private void Awake()
	{
		if (!RuntimeManager.IsInitialized())
		{
			RuntimeManager.Init();
		}

		Init();

		StartCoroutine(ShowInterstitalAds());
	}

	private void OnEnable()
	{
		Subsribe();
	}

	private void OnDisable()
	{
		GeneralControls.ControlQuit(Unsubsribe);
	}

	private void Init()
	{
		m_WaitForInterstitialAd = new WaitUntil(() => Advertising.IsInterstitialAdReady());
		m_WaitForRewardedAd = new WaitUntil(() => Advertising.IsRewardedAdReady());
	}

	#region Event Subsribe/Unsubscribe

	private void Subsribe() 
	{
		Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
		Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
	}

	private void Unsubsribe()
	{
		Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
		Advertising.RewardedAdSkipped -= RewardedAdSkippedHandler;
	}

	#endregion

	public void ShowBannerAds()
	{
		Advertising.ShowBannerAd(BannerAdPosition.Bottom);
	}

	public IEnumerator ShowInterstitalAds()
	{
		Debug.LogError("İlk debug");

		yield return m_WaitForInterstitialAd;

		Debug.LogError("Son debug");
		Advertising.ShowInterstitialAd();
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

		StartCoroutine(EventManager.Instance.GameOverTrigger?.Invoke(GameOverType.WrongAnswer));
	}
}
