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
    }
    public void AddEarnedMinigameCoins()
    {
        _coins += MinigameRewardCalculator.instance.GetMinigameCurrencyAmount();
    }
    public bool TrySpendCoins(int amount)
    {

        return true;
    }
    private void SaveCurrency()
    {
        PlayerPrefs.SetInt("Coins", _coins);
        PlayerPrefs.Save();
    }
    private void LoadCurrency()
    {
        PlayerPrefs.GetInt("Coins", 0);
    }
}
