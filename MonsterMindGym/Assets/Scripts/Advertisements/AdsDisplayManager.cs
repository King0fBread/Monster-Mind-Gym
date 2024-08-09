using UnityEngine;


public class AdsDisplayManager : MonoBehaviour
{
    public InitializeAds initializeAds;
    public RewardedAds rewardedAds;
    public InterstitialAds interstitialAds;

    public static AdsDisplayManager Instance { get { return _instance; } }
    private static AdsDisplayManager _instance;
    private void Awake()
    {
        if(_instance != null & _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            rewardedAds.LoadRewardedlAd();
            interstitialAds.LoadInterstitialAd();
        }
    }
    public void DisplayRewardAd()
    {
        //
    }
}
