using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class MonsterUpgradeManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _monsterEvoluitionStages;
    [SerializeField] private CurrencyManager _currencyManager;

    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _bonusTipText;

    [SerializeField] private Button _upgradeButton;

    [SerializeField] List<string> _levelAddresses;

    private AsyncOperationHandle<LevelData>? _currentLevelHandle;

    private LevelData _currentLevelData;

    private int _currentMonsterLevel;


    private void Awake()
    {
        LoadLevelInfo();
        AssignUpgradeButton();
    }
    private void LoadLevelInfo()
    {
        _currentMonsterLevel = PlayerPrefs.GetInt("MonsterLevel", 1);
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
    private void AssignUpgradeButton()
    {
        _upgradeButton.onClick.RemoveAllListeners();
        _upgradeButton.onClick.AddListener(TryUpgradeLevel);
    }
    private void OnLevelDataLoaded(AsyncOperationHandle<LevelData> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            _currentLevelData = handle.Result;
        }
    }
    private void DisplayLevelInfo()
    {
        _currentLevelText.text = $"Level {_currentLevelData.level}";
        
    }
    public void TryUpgradeLevel()
    {
        if(_currentMonsterLevel<_levelAddresses.Count-1
            && _currencyManager.TrySpendCoins(_currentLevelData.upgradeCost))
        {
            _currentMonsterLevel++;
        }

    }


}
