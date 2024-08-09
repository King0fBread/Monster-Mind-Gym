using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameID;
    [SerializeField] private bool _isTesting;

    private string _gameID;

    private void Awake()
    {
#if UNITY_ANDROID
        _gameID = _androidGameID;
#endif

        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {

            Advertisement.Initialize(_gameID, _isTesting, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Ads init successful");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Ads init falied");
    }

}
