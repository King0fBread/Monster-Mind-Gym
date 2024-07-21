using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsDisplayText;
    [SerializeField] private GameObject _coinsCollectionAnimationObject;

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
        _coinsCollectionAnimationObject.SetActive(false);
        _coinsCollectionAnimationObject.SetActive(true);
        SaveCurrency();
    }
    public void AddEarnedMinigameCoins()
    {
        _coins += MinigameRewardCalculator.instance.GetMinigameCurrencyAmount();

        _coinsCollectionAnimationObject.SetActive(false);
        _coinsCollectionAnimationObject.SetActive(true);

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
