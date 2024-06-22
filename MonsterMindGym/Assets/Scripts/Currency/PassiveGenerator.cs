using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class PassiveGenerator : MonoBehaviour
{
    [SerializeField] private CurrencyManager _currencyManager;
    [SerializeField] private Button _generatorButton;

    private int _currentStatus;
    private int _currentMaxAmount;
    private int _currentHeldAmount;

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

        SubscribeButtonToMethod();

        CalculateOfflineEarnings();
    }
    private void LoadGeneratorInfo()
    {
        _currentStatus = PlayerPrefs.GetInt("ButtonStatus", 0);
        _currentHeldAmount = PlayerPrefs.GetInt("HeldAmount", 0);

        _currentMaxAmount = _generatorStages[_currentStatus].maxAmount;
        _currentTimeToGain = _generatorStages[_currentStatus].secondsToGainCurrency;
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
    private void SubscribeButtonToMethod()
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
        }
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
            _currentTimeToGain = _generatorStages[0].secondsToGainCurrency;
            _currentMaxAmount = _generatorStages[0].maxAmount;

            SaveGeneratorInfo();
            SubscribeButtonToMethod();
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
            }
        }
        else
        {
            print("not enough for upgrade");
        }
    }
    private bool TryPurchaseGenerator()
    {
        if (_currencyManager.TrySpendCoins(_generatorStages[_currentStatus].price))
        {
            return true;
        }
        else
        {
            return false;
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
            _currentHeldAmount = Mathf.Min(_currentHeldAmount + earnedWhileAway, _currentMaxAmount);
        }
    }
    private void OnApplicationQuit()
    {
        SaveGeneratorInfo();
        SaveAppClosingTime();
    }
}
