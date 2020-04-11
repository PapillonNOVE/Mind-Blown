using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

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
}
