using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostGameBonus : MonoBehaviour
{
    [SerializeField] private CurrencyManager _currencyManager;
    [SerializeField] private int _increaseRate;

    [SerializeField] private GameObject _trainObject;

    private int _bonusAmount;
    
    private void Awake()
    {
        LoadBonusInfo();

        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        gameObject.GetComponent<Button>().onClick.AddListener(ClaimBonus);
    }
    private void LoadBonusInfo()
    {
        _bonusAmount = PlayerPrefs.GetInt("PostGameBonus", 10);
    }
    private void ClaimBonus()
    {
        _currencyManager.AddCoins(_bonusAmount);

        _trainObject.SetActive(true);
        gameObject.SetActive(false);

        SoundsManager.Instance.PlaySound(SoundsManager.Sounds.CoinsCollect);
    }
    public void IncreaseBonusAmount()
    {
        _bonusAmount += _increaseRate;

        PlayerPrefs.SetInt("PostGameBonus", _bonusAmount);
        PlayerPrefs.Save();
    }
}
