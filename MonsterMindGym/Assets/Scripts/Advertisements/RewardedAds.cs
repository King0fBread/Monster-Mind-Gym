using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnitID;

    private string _adUnitID;
    private void Awake()
    {
#if UNITY_ANDROID
        _adUnitID = _androidAdUnitID;
#endif
    }
    public void LoadRewardedlAd()
    {
        Advertisement.Load(_adUnitID, this);
    }
    public void ShowRewardedlAd()
    {
        Advertisement.Show(_adUnitID, this);
        LoadRewardedlAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Rewarded ad loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Rewarded ad failed to load");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Rewarded ad show failed");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Rewarded ad show started");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Interstitial ad show clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(placementId == _androidAdUnitID && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED)) 
        {
            //ad fully watched
        }
    }
}
