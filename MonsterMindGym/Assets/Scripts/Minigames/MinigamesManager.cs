using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigamesManager : MonoBehaviour
{
    [SerializeField] private GameObject _backgroundObject;
    [SerializeField] private GameObject _currencyObject;
    [SerializeField] private GameObject _getReadyScreenObject;
    [SerializeField] private GameObject[] _minigameObjects;

    [SerializeField] private TextMeshProUGUI _upcomingGameText;

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

        SetCurrentMinigameInstance();
    }
    private void SetCurrentMinigameInstance()
    {
        _startButton.gameObject.SetActive(false);
        _getReadyScreenObject.SetActive(true);
        _currencyObject.gameObject.SetActive(false);

        _backgroundObject.gameObject.SetActive(true);

        _upcomingGameText.text = "Upcoming game: " + _currentMinigame.name;

        _getReadyScreenObject.GetComponent<DisableSelfOrSpecified>().objectToToggle = _currentMinigame;
    }
    private void FinishCurrentMinigameInstance()
    {
        _currencyObject.gameObject.SetActive(true);
        _backgroundObject.gameObject.SetActive(false);

        _currentMinigame = null;
        _currentMinigame.SetActive(false);
    }
}
