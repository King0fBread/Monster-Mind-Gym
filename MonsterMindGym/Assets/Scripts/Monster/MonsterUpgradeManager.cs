using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.UI;

public class MonsterUpgradeManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _monsterEvoluitionStages;
    [SerializeField] private CurrencyManager _currencyManager;

    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _bonusTipText;

    [SerializeField] private Button _upgradeButton;

    [SerializeField] private string _levelsGroupLabel;

    //[SerializeField] List<string> _levelAddresses;

    private AsyncOperationHandle<LevelData>? _currentLevelHandle;

    private LevelData _currentLevelData;

    private int _currentMonsterLevel;

    private int _totalLevelsCount;


    private void Awake()
    {
        LoadLevelInfo();

        CountTotalLevels();

        LoadCurrentLevel();
        AssignUpgradeButton();
    }
    private void LoadLevelInfo()
    {
        _currentMonsterLevel = PlayerPrefs.GetInt("MonsterLevel", 1);
    }
    private void AssignUpgradeButton()
    {
        _upgradeButton.onClick.RemoveAllListeners();
        _upgradeButton.onClick.AddListener(TryUpgradeLevel);
    }
    private void LoadCurrentLevel()
    {

        if (_currentLevelHandle.HasValue)
        {
            Addressables.Release(_currentLevelHandle.Value);
        }
        _currentLevelHandle = Addressables.LoadAssetAsync<LevelData>($"Level_{_currentMonsterLevel}");
        _currentLevelHandle.Value.Completed += OnLevelDataLoaded;
    }
    private void CountTotalLevels()
    {
        Addressables.LoadResourceLocationsAsync(_levelsGroupLabel).Completed += OnLevelsAmountLoadedCompleted;
    }
    private void OnLevelDataLoaded(AsyncOperationHandle<LevelData> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            _currentLevelData = handle.Result;
        }
        DisplayLevelInfo();
    }
    private void OnLevelsAmountLoadedCompleted(AsyncOperationHandle<IList<IResourceLocation>> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {

            IList<IResourceLocation> locations = handle.Result;
            _totalLevelsCount = locations.Count;
            print("total lvls " + _totalLevelsCount);
        }
    }
    private void DisplayLevelInfo()
    {
        _currentLevelText.text = $"Level {_currentLevelData.level}";
        _bonusTipText.text = "Bonus TODO";
        
    }
    public void TryUpgradeLevel()
    {
        if(_currentMonsterLevel<_totalLevelsCount-1
            && _currencyManager.TrySpendCoins(_currentLevelData.upgradeCost))
        {
            _currentMonsterLevel++;
        }

    }
    private void TryUpgradeMonsterStats()
    {

    }


}
