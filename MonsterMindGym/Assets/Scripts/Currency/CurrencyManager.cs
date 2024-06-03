using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private int _coins;
    private void Awake()
    {
        LoadCurrency();
    }
    public void AddCoins(int amount)
    {
        
    }
    public bool TrySpendCoins(int amount)
    {

        return true;
    }
    private void SaveCurrency()
    {
        PlayerPrefs.SetInt("Coins", _coins);
    }
    private void LoadCurrency()
    {
        PlayerPrefs.GetInt("Coins", 0);
    }
}
