using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameRewardCalculator : MonoBehaviour
{
    [SerializeField] private CurrencyManager _currencyManager;

    private static MinigameRewardCalculator _instance;
    public static MinigameRewardCalculator instance { get { return _instance; } }

    private int _currencyAmount;
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void CalculateInitialEarnedCurrency(int earnedPoints)
    {
        _currencyAmount = earnedPoints;
    }
    public void CalculateElixirMultipliedCurrency(float multiplier)
    {
        float _modifiedCurrencyPrecise = _currencyAmount * multiplier;
        _currencyAmount = Convert.ToInt32(_modifiedCurrencyPrecise);

        RewardScreenManager.instance.DisplayMultipliedPoints(_currencyAmount);
    }
    public int GetMinigameCurrencyAmount()
    {
        return _currencyAmount;
    }
}
