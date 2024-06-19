using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class BackToLabButton : MonoBehaviour
{
    [SerializeField] private GameObject _currencyObject;
    [SerializeField] private GameObject _minigameMenuObject;
    [SerializeField] private GameObject _backgroundObject;

    [SerializeField] private PostGameBonus _postGameBonusButton;

    [SerializeField] private CurrencyManager _currencyManager;
    private void Awake()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(PerformBackToLabSequence);
    }
    private void PerformBackToLabSequence()
    {
        _currencyObject.SetActive(true);
        _minigameMenuObject.SetActive(false);
        _backgroundObject.SetActive(false);

        _postGameBonusButton.gameObject.SetActive(true);

        _currencyManager.AddEarnedMinigameCoins();

    }
}
