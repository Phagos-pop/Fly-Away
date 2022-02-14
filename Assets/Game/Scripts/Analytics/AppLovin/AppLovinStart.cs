using System;
using UnityEngine;

public class AppLovinStart : MonoBehaviour
{
    FirebaseRemoteConfigStart remoteConfig;

    string interstitialAdUnitId = "01b0b364bffe0960";
    int interstitialRetryAttempt;

    string bannerAdUnitId = "06df718d586aee81";
    bool isBannerShown = false;

    string rewardAdUnitId = "c6348a13bd76eeff";
    int rewardRetryAttempt;

    void Start()
    {
        remoteConfig = GetComponent<FirebaseRemoteConfigStart>();

        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            
        };

        MaxSdk.SetSdkKey("CvFQ6rVnxKktdZBfCvb-FTbPJcBiFOV7Fp94Oau3NX4O5ywf5948NLCrRGKdKSH9vg2zI_AyEuxLTP9-wIZZDn");
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
        InitializeBannerAds();
        InitializeInterstitialAds();
        InitializeRewardedAds();

        Messenger.AddListener(GameEvent.SHOW_REWARD, ShowReward);
        Messenger.AddListener(GameEvent.SHOW_INTERSTITIAL, ShowInterstitial);
        Messenger.AddListener(GameEvent.SHOW_BANNER, ShowBanner);
        Messenger.AddListener(GameEvent.HIDE_BANNER, HideBanner);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SHOW_INTERSTITIAL, ShowInterstitial);
    }

    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
        // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);

        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.clear);
    }

    private void ShowBanner()
    {
        if (!isBannerShown)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("banner_start");
            MaxSdk.ShowBanner(bannerAdUnitId);
            isBannerShown = true;
        }
    }

    private void HideBanner()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("banner_closed");
        MaxSdk.HideBanner(bannerAdUnitId);
        isBannerShown = false;
    }
    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(interstitialAdUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        interstitialRetryAttempt = 0;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        interstitialRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("inter_shown");
    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("inter_fail");
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial();
    }

    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        LoadInterstitial();
    }

    private void ShowInterstitial()
    {
        if (MaxSdk.IsInterstitialReady(interstitialAdUnitId) && remoteConfig.IsInterTimeIntervalPassed)
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent("inter_start");
            MaxSdk.ShowInterstitial(interstitialAdUnitId);
            remoteConfig.StartInterstitialTimer();
        }
    }

    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first rewarded ad
        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(rewardAdUnitId);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        rewardRetryAttempt = 0;
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        rewardRetryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, rewardRetryAttempt));

        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Rew_shown");
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("Rew_failed");
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadRewardedAd();
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Ad revenue paid. Use this callback to track user revenue.
    }

    private void ShowReward()
    {
        if (MaxSdk.IsRewardedAdReady(rewardAdUnitId))
        {
            MaxSdk.ShowRewardedAd(rewardAdUnitId);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Rew_start");
        }
    }
}
