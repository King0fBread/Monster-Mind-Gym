using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    private static EnergyManager _instance;
    public static EnergyManager Instance { get { return _instance; } }

    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _energyCounterText;

    private const string _energyRecoveryTimeInMinutesKey = "energyRecoverySpeedKey";
    private const string _maxEnergyKey = "maxEnergyKey";
    private const string _currentEnergyKey = "currentEnergyKey";
    private const string _lastEnergyUpdateKey = "lastEnergyUpdate";

    private int _maxEnergy;
    private int _currentEnergy;
    private float _energyRecoveryTimeInMinutes;
    private DateTime _lastEnergyUpdate;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        LoadEnergyData();
        UpdateEnergyData();

        InvokeRepeating("UpdateTimer", 0f, 1f);
    }
    private void Update()
    {
        UpdateEnergyData();
        UpdateEnergyCounter();
    }
    private void LoadEnergyData()
    {
        _maxEnergy = PlayerPrefs.GetInt(_maxEnergyKey, 10);
        _energyRecoveryTimeInMinutes = float.Parse(PlayerPrefs.GetString(_energyRecoveryTimeInMinutesKey, "5"));
        _currentEnergy = PlayerPrefs.GetInt(_currentEnergyKey, _maxEnergy);

        string lastEnergyUpdateString = PlayerPrefs.GetString(_lastEnergyUpdateKey, DateTime.Now.ToString());
        _lastEnergyUpdate = DateTime.Parse(lastEnergyUpdateString);
    }
    private void SaveEnergyData()
    {
        PlayerPrefs.SetInt(_currentEnergyKey, _currentEnergy);
        PlayerPrefs.SetString(_lastEnergyUpdateKey, _lastEnergyUpdate.ToString());
        PlayerPrefs.Save();
    }
    private void UpdateEnergyData()
    {
        if(_currentEnergy < _maxEnergy)
        {
            TimeSpan timeSpan = DateTime.Now - _lastEnergyUpdate;
            int minutesPassed = (int)timeSpan.TotalMinutes;

            if(minutesPassed >= _energyRecoveryTimeInMinutes)
            {
                int energyToRecover = Convert.ToInt32(minutesPassed / _energyRecoveryTimeInMinutes);

                _currentEnergy += 1;
                _lastEnergyUpdate = _lastEnergyUpdate.AddMinutes(energyToRecover * _energyRecoveryTimeInMinutes);
                SaveEnergyData();
            }
        }
    }
    private void UpdateEnergyCounter()
    {
        _energyCounterText.text = $"{_currentEnergy}/{_maxEnergy}";
    }
    private void UpdateTimer()
    {
        if(_currentEnergy < _maxEnergy)
        {
            TimeSpan timePassed = DateTime.Now - _lastEnergyUpdate;
            int secondsPassed = (int)timePassed.TotalSeconds;
            int secondsToNextEnergy = (int)_energyRecoveryTimeInMinutes * 60 - secondsPassed;

            if(secondsToNextEnergy <= 0)
            {
                UpdateEnergyData();
                secondsToNextEnergy = (int)_energyRecoveryTimeInMinutes * 60;
            }

            int minutes = secondsToNextEnergy / 60;
            int seconds = secondsToNextEnergy % 60;

            _timerText.text = $"{minutes:D2}:{seconds:D2}";
        }
        else
        {
            _timerText.text = " ";
        }
    }
    public bool SpendEnergy()
    {
        if(_currentEnergy >= 1)
        {
            _currentEnergy -= 1;
            SaveEnergyData();

            return true;
        }
        else
        {
            return false;
        }
    }
    public void UprgadeMaxEnergyAmount()
    {
        _maxEnergy += 1;
        PlayerPrefs.SetInt(_maxEnergyKey, _maxEnergy);
    }
    public void UpgradeEnergyRecovery()
    {
        _energyRecoveryTimeInMinutes -= 0.5f;
        string energyRecoveryString = _energyRecoveryTimeInMinutes.ToString("0.0");
        PlayerPrefs.SetString(_energyRecoveryTimeInMinutesKey, energyRecoveryString);
    }

}
