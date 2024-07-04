using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    [SerializeField] private PostGameBonus _postGameBonus;
    [SerializeField] private MinigamesManager _minigameManager;

    [SerializeField] private SpriteRenderer _monsterSpriteRenderer;

    //[SerializeField] List<string> _levelAddresses;

    private AsyncOperationHandle<LevelData>? _currentLevelHandle;

    private LevelData _currentLevelData;

    private int _currentMonsterLevel;

    private int _totalLevelsCount;

    private int _currentVisualMonsterID;

    private bool _playerHasClaimedLevelUpgrades = true;


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
        _currentVisualMonsterID = PlayerPrefs.GetInt("MonsterVisualID", 0);
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

            DisplayLevelInfo();
            DisplayMonsterVisual();
            TryUpgradeMonsterStats();
        }
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
    private void DisplayMonsterVisual()
    {
        _monsterSpriteRenderer.sprite = _monsterEvoluitionStages[_currentVisualMonsterID];
    }
    public void TryUpgradeLevel()
    {
        if(_currentMonsterLevel<_totalLevelsCount-1 && _currencyManager.TrySpendCoins(_currentLevelData.upgradeCost))
        {
            _currentMonsterLevel++;
            PlayerPrefs.SetInt("MonsterLevel", _currentMonsterLevel);
            _playerHasClaimedLevelUpgrades = false;

            LoadCurrentLevel();
        }
        else
        {
            print("not enough coins!");
        }

    }
    private void TryUpgradeMonsterStats()
    {
        if (_playerHasClaimedLevelUpgrades)
        {
            return;
        }

        _playerHasClaimedLevelUpgrades = true;

        if(_currentLevelData.maxEnergyIncrease != 0)
        {
            EnergyManager.Instance.UpgradeMaxEnergyAmount();
        }
        if(_currentLevelData.energyRecoveryDecrease != 0)
        {
            EnergyManager.Instance.UpgradeEnergyRecovery();
        }
        if (_currentLevelData.unlockNextMinigame)
        {
            _minigameManager.UnlockNextMinigame();
        }
        if (_currentLevelData.increasePostMinigameBonus)
        {
            _postGameBonus.IncreaseBonusAmount();
        }
        if (_currentLevelData.visuallyUpgradeMonster)
        {
            _currentVisualMonsterID++;
            PlayerPrefs.SetInt("MonsterVisualID", _currentVisualMonsterID);
            DisplayMonsterVisual();
        }
    }


}
