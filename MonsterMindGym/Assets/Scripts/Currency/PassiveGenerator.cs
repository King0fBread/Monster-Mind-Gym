using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveGenerator : MonoBehaviour
{
    [SerializeField] private CurrencyManager _currencyManager;

    [SerializeField] private Button _generatorButton;
    [SerializeField] private Button _claimButton;

    [SerializeField] private TextMeshProUGUI _priceDisplayText;
    [SerializeField] private TextMeshProUGUI _storageAmountText;

    [SerializeField] private TextMeshProUGUI _coinPerSecondsText;
    [SerializeField] private TextMeshProUGUI _maxAmountText;

    [SerializeField] private GameObject _generatorBuyObject;

    [SerializeField] private GameObject _generatorParticleSystem;
    [SerializeField] private GameObject _generatorInteractionEffectObject;

    private int _currentStatus;
    private int _currentMaxAmount;
    private int _currentHeldAmount = 0;

    private float _currentTimeToGain;

    private float _timer = 0f;

    public GeneratorStages[] _generatorStages;
    [System.Serializable]
    public class GeneratorStages
    {
        public int price;
        public float secondsToGainCurrency;
        public int maxAmount;
    }
    private void Awake()
    {
        LoadGeneratorInfo();

        SubscribeGeneratorButton();
        SubscribeClaimButton();

        TryDisplayParticles();

        if (_currentStatus > 0)
        {
            CalculateOfflineEarnings();
        }

        InvokeRepeating("DisplayGeneratorInfo", 1, 1);

    }
    private void LoadGeneratorInfo()
    {
        _currentStatus = PlayerPrefs.GetInt("ButtonStatus", 0);
        _currentHeldAmount = PlayerPrefs.GetInt("HeldAmount", 0);

        if (_currentStatus == _generatorStages.Length - 1)
        {
            _generatorBuyObject.SetActive(false);
        }
        _currentMaxAmount = _generatorStages[_currentStatus].maxAmount;
        _currentTimeToGain = _generatorStages[_currentStatus].secondsToGainCurrency;
    }
    private void TryDisplayParticles()
    {
        if(_currentStatus > 0)
        {
            _generatorParticleSystem.SetActive(true);
        }
    }
    private void SaveGeneratorInfo()
    {
        PlayerPrefs.SetInt("ButtonStatus", _currentStatus);
        PlayerPrefs.SetInt("HeldAmount", _currentHeldAmount);
    }
    private void SaveAppClosingTime()
    {
        PlayerPrefs.SetString("LastClosedTime", DateTime.Now.ToString());
    }
    private void SubscribeGeneratorButton()
    {

        if(_currentStatus == 0)
        {
            _generatorButton.onClick.RemoveAllListeners();
            _generatorButton.onClick.AddListener(BuyGenerator);
        }
        else
        {
            _generatorButton.onClick.RemoveAllListeners();
            _generatorButton.onClick.AddListener(UpgradeGenerator);

            _generatorButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade generator";
        }
    }
    private void SubscribeClaimButton()
    {
        _claimButton.onClick.RemoveAllListeners();
        _claimButton.onClick.AddListener(ClaimHeldCurrency);
    }
    private void Update()
    {
        if (_currentStatus <= 0)
            return;

        _timer += Time.deltaTime;

        if(_timer >= _currentTimeToGain && _currentHeldAmount < _currentMaxAmount)
        {
            _timer = 0; 
            _currentHeldAmount++;
        }
    }
    private void BuyGenerator()
    {
        if (TryPurchaseGenerator())
        {
            _currentStatus = 1;
            _currentTimeToGain = _generatorStages[_currentStatus].secondsToGainCurrency;
            _currentMaxAmount = _generatorStages[_currentStatus].maxAmount;

            SaveGeneratorInfo();
            SubscribeGeneratorButton();
            TryDisplayParticles();

            _generatorInteractionEffectObject.SetActive(true);
            SoundsManager.Instance.PlaySound(SoundsManager.Sounds.GeneratorInteraction);
        }
        else
        {
            print("not enough for buying");
        }

    }
    private void UpgradeGenerator()
    {
        if (TryPurchaseGenerator())
        {
            _currentStatus++;

            if(_currentStatus < _generatorStages.Length)
            {
                _currentTimeToGain = _generatorStages[_currentStatus].secondsToGainCurrency;
                _currentMaxAmount = _generatorStages[_currentStatus].maxAmount;

                SaveGeneratorInfo();

                _generatorInteractionEffectObject.SetActive(true);
                SoundsManager.Instance.PlaySound(SoundsManager.Sounds.GeneratorInteraction);
            }
        }
    }
    private bool TryPurchaseGenerator()
    {
        if(_currentStatus < _generatorStages.Length-1)
        {
            if (_currencyManager.TrySpendCoins(_generatorStages[_currentStatus].price))
            {
                return true;
            }
            else
            {
                print("not enough for upgrade");
                return false;
            }
        }
        else
        {
            print("max level generator");
            _generatorBuyObject.SetActive(false);
            return false;
        }
    }
    private void ClaimHeldCurrency()
    {
        if(_currentHeldAmount > 0)
        {
            _currencyManager.AddCoins(_currentHeldAmount);
            _currentHeldAmount = 0;

            SoundsManager.Instance.PlaySound(SoundsManager.Sounds.CoinsCollect);

            SaveGeneratorInfo();

        }
    }
    private void CalculateOfflineEarnings()
    {
        string lastClosedTimeString = PlayerPrefs.GetString("LastClosedTime", string.Empty);

        if (!string.IsNullOrEmpty(lastClosedTimeString))
        {
            DateTime lastClosedTime = DateTime.Parse(lastClosedTimeString);
            TimeSpan timeAway = DateTime.Now - lastClosedTime;

            int earnedWhileAway = (int)(timeAway.TotalSeconds / _currentTimeToGain);

            if(earnedWhileAway + _currentHeldAmount >= _currentMaxAmount)
            {
                _currentHeldAmount = _currentMaxAmount;
            }
            else
            {
                _currentHeldAmount += earnedWhileAway;
            }
        }
    }
    private void DisplayGeneratorInfo()
    {
        _storageAmountText.text = $"{_currentHeldAmount}/{_currentMaxAmount}";
        _coinPerSecondsText.text = $"1/{_currentTimeToGain}s";
        _maxAmountText.text = $"max {_currentMaxAmount}";
        _priceDisplayText.text = _generatorStages[_currentStatus].price.ToString();
    }
    private void OnApplicationQuit()
    {
        SaveGeneratorInfo();
        SaveAppClosingTime();
    }
}
