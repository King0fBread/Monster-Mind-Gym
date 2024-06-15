using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    private static EnergyManager _instance;
    public static EnergyManager Instance { get { return _instance; } }


    private const string _energyRecoveryTimeInMinutesKey = "energyRecoverySpeedKey";
    private const string _maxEnergyKey = "maxEnergyKey";
    private const string _currentEnergyKey = "currentEnergyKey";
    private const string _lastEnergyUpdateKey = "lastEnergyUpdate";

    private int _maxEnergy;
    private int _energyRecoveryTimeInMinutes;
    private int _currentEnergy;
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
        
    }
    private void Update()
    {
        UpdateEnergyData();
    }
    private void LoadEnergyDate()
    {
        _maxEnergy = PlayerPrefs.GetInt(_maxEnergyKey, 10);
        _energyRecoveryTimeInMinutes = PlayerPrefs.GetInt(_energyRecoveryTimeInMinutesKey, 5);
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
                _currentEnergy += 1;
                _lastEnergyUpdate = DateTime.Now - TimeSpan.FromMinutes(minutesPassed % _energyRecoveryTimeInMinutes);
                SaveEnergyData();
            }
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

}
