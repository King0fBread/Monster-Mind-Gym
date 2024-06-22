using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PassiveGenerator : MonoBehaviour
{
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
    }
    private void LoadGeneratorInfo()
    {
        _currentStatus = PlayerPrefs.GetInt("ButtonStatus", 0);
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
            _currentHeldAmount++;
        }
    }
    private void BuyGenerator()
    {
        _currentStatus = 1;
        _currentTimeToGain = _generatorStages[0].secondsToGainCurrency;
        _currentMaxAmount = _generatorStages[0].maxAmount;
    }
    private void UpgradeGenerator()
    {
        _currentStatus++;

        if(_currentStatus < _generatorStages.Length)
        {
            _currentTimeToGain = _generatorStages[_currentStatus].secondsToGainCurrency;
            _currentMaxAmount = _generatorStages[_currentStatus].maxAmount;
        }
    }
}
