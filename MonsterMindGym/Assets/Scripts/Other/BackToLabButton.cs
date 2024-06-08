using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class BackToLabButton : MonoBehaviour
{
    [SerializeField] private CurrencyManager _currencyManager;
    [SerializeField] private GameObject _minigameMenuObject;
    [SerializeField] private RoomTransitionsManager _roomTransitionsManager;
    private void Awake()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(PerformBackToLabSequence);
    }
    private void PerformBackToLabSequence()
    {
        _minigameMenuObject.SetActive(false);

        _currencyManager.AddEarnedMinigameCoins();

        _roomTransitionsManager.MoveToLeftRoom();
    }
}
