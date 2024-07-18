using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Lumin;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsDisplayText;

    private int _coins;
    private bool _currentCoinsAreDisplayed;
    private void Awake()
    {
        AddCoins(1000000);
        LoadCurrency();
        _currentCoinsAreDisplayed = false;
    }
    private void Update()
    {
        if (_currentCoinsAreDisplayed)
            return;

        _coinsDisplayText.text = _coins.ToString();
    }
    public void AddCoins(int amount)
    {
        _coins += amount;
        SaveCurrency();
    }
    public void AddEarnedMinigameCoins()
    {
        _coins += MinigameRewardCalculator.instance.GetMinigameCurrencyAmount();
        SaveCurrency();
    }
    public bool TrySpendCoins(int amount)
    {
        if(_coins >= amount)
        {
            _coins -= amount;
            SaveCurrency();

            return true;
        }
        return false;
    }
    private void SaveCurrency()
    {
        PlayerPrefs.SetInt("Coins", _coins);
    }
    private void LoadCurrency()
    {
        _coins = PlayerPrefs.GetInt("Coins", 0);
    }
    public int GetCurrency()
    {
        return _coins;
    }
}
