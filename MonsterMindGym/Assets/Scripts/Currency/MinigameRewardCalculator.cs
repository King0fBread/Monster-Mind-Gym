using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameRewardCalculator : MonoBehaviour
{
    [SerializeField] private CurrencyManager _currencyManager;

    private static MinigameRewardCalculator _instance;
    public static MinigameRewardCalculator instance { get { return _instance; } }

    private int _initialCurrencyAmount;
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
        _initialCurrencyAmount = earnedPoints;
    }
}
