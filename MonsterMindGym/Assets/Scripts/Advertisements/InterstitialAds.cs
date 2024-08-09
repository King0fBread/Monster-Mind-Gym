using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnitID;

    private string _adUnitID;
    private void Awake()
    {
#if UNITY_ANDROID
        _adUnitID = _androidAdUnitID;
#endif
    }

    public void LoadInterstitialAd()
    {
        Advertisement.Load(_adUnitID, this);
    }
    public void ShowInterstitialAd()
    {
        Advertisement.Show(_adUnitID, this);
        LoadInterstitialAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial ad loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Interstitial ad failed to load");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Interstitial ad show failed");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Interstitial ad show started");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Interstitial ad show clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Interstitial ad show completed");
    }
}
