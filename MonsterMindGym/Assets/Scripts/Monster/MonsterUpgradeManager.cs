using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private TextMeshProUGUI _levelUpgradePriceText;

    [SerializeField] private Button _upgradeButton;

    [SerializeField] private string _levelsGroupLabel;

    [SerializeField] private PostGameBonus _postGameBonus;
    [SerializeField] private MinigamesManager _minigameManager;

    [SerializeField] private SpriteRenderer _monsterSpriteRenderer;

    [SerializeField] UpgradeDescriptionPopup _upgradePopup;

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

        //ResetMonsterDEVELOPMENT();
    }
    private void LoadLevelInfo()
    {
        _currentMonsterLevel = PlayerPrefs.GetInt("MonsterLevel", 1);
        _currentVisualMonsterID = PlayerPrefs.GetInt("MonsterVisualID", 0);
    }
    private void ResetMonsterDEVELOPMENT()
    {
        _currentMonsterLevel = 1;
        _currentVisualMonsterID  = 0;
        EnergyManager.Instance.ResetEnergyDEVELOPMENT();
        _minigameManager.ResetUnlockedMinigamesDEVELOPMENT();
        _postGameBonus.ResetBonusAmountDEVELOPMENT();
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

            DisplayMonsterVisual();
            DisplayLevelInfo();
            DisplaySpecialEffect();
            TryUpgradeMonsterStats();

            if (RequestedLevelHasUpgrades(_currentLevelData))
            {
                _upgradePopup.DisplayUpgradePopup(_currentLevelData);
            }
        }
    }
    private void OnLevelsAmountLoadedCompleted(AsyncOperationHandle<IList<IResourceLocation>> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {

            IList<IResourceLocation> locations = handle.Result;
            _totalLevelsCount = locations.Count;
        }
    }
    private void DisplayLevelInfo()
    {
        _currentLevelText.text = $"Level {_currentLevelData.level}";
        _levelUpgradePriceText.text = $"{_currentLevelData.upgradeCost}";

        StartCoroutine(CheckNextBonusLevel(_currentMonsterLevel));
    }
    private void DisplayMonsterVisual()
    {
        _monsterSpriteRenderer.sprite = _monsterEvoluitionStages[_currentVisualMonsterID];
    }
    private void DisplaySpecialEffect()
    {
        if (_currentLevelData.visuallyUpgradeMonster)
        {
            SpecialEffectsManager.Instance.DisplayMonsterVisualUpgradeEffect();
        }
        else
        {
            SpecialEffectsManager.Instance.DisplayMonsterBasicUpgradeEffect();
        }
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
            print("not enough coins or max lvl");
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
        //notification about the bonus
    }
    private IEnumerator CheckNextBonusLevel(int currentLevel)
    {
        for (int i = 1; i < _totalLevelsCount; i++)
        {
            var handle = Addressables.LoadAssetAsync<LevelData>($"Level_{currentLevel + i}");
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                LevelData levelData = handle.Result;
                if (RequestedLevelHasUpgrades(levelData))
                {
                    _bonusTipText.text = $"Upgrade in {i} levels";
                    Addressables.Release(handle);
                    yield break;
                }
                Addressables.Release(handle);
            }
            else
            {
                Debug.LogError($"Failed to load LevelData: {handle.Status}");
            }
        }

        _bonusTipText.text = "MAX LEVEL";
    }
    private bool RequestedLevelHasUpgrades(LevelData requestedLevelData)
    {
        if (requestedLevelData.maxEnergyIncrease != 0)
            return true;
        if (requestedLevelData.energyRecoveryDecrease != 0)
            return true;
        if (requestedLevelData.unlockNextMinigame)
            return true;
        if (requestedLevelData.visuallyUpgradeMonster)
            return true;

        return false;
    }


}
