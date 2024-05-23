using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class MinigamesManager : MonoBehaviour
{
    [SerializeField] private GameObject _backgroundObject;
    [SerializeField] private GameObject _currencyObject;
    [SerializeField] private GameObject _getReadyScreenObject;
    [SerializeField] private GameObject[] _minigameObjects;


    [SerializeField] private Button _startButton;

    private GameObject _currentMinigame;
    private void Awake()
    {
        _startButton.onClick.RemoveAllListeners();
        _startButton.onClick.AddListener(InitiateRandomMinigame);
    }
    private void InitiateRandomMinigame()
    {
        _currentMinigame = _minigameObjects[Random.Range(0, _minigameObjects.Length)];

        SetRelatedObjectsStates();
    }
    private void SetRelatedObjectsStates()
    {
        _startButton.gameObject.SetActive(false);
        _getReadyScreenObject.SetActive(true);
        _currencyObject.gameObject.SetActive(false);

        _backgroundObject.gameObject.SetActive(true);
        _currentMinigame.SetActive(true);
    }
}
