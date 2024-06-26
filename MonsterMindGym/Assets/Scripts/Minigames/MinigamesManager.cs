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
    [SerializeField] private GameObject[] _totalMinigamesObject;

    [SerializeField] private TextMeshProUGUI _upcomingGameText;

    [SerializeField] private GameObject _startMinigameObject;

    private List<GameObject> _unlockedMinigamesList;

    private GameObject _currentMinigame;
    private void Awake()
    {
        _startMinigameObject.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        _startMinigameObject.GetComponentInChildren<Button>().onClick.AddListener(TrySpendEnergyAndStartMinigame);
    }
    private void LoadUnlockedMinigames()
    {


    }
    private void InitiateRandomMinigame()
    {
        _currentMinigame = _totalMinigamesObject[Random.Range(0, _totalMinigamesObject.Length)];

        SetCurrentMinigameInstance();
    }
    private void SetCurrentMinigameInstance()
    {
        _startMinigameObject.SetActive(false);
        _getReadyScreenObject.SetActive(true);
        _currencyObject.gameObject.SetActive(false);

        _backgroundObject.gameObject.SetActive(true);

        _upcomingGameText.text = "Upcoming game: " + _currentMinigame.name;

        _getReadyScreenObject.GetComponent<DisableSelfOrSpecified>().objectToToggle = _currentMinigame;
    }
    private void TrySpendEnergyAndStartMinigame()
    {
        bool hasEnoughEnergy = EnergyManager.Instance.SpendEnergy();
        if (hasEnoughEnergy)
        {
            InitiateRandomMinigame();
        }
        else
        {

        }

    }
    private void FinishCurrentMinigameInstance()
    {
        _currencyObject.gameObject.SetActive(true);
        _backgroundObject.gameObject.SetActive(false);

        _currentMinigame = null;
        _currentMinigame.SetActive(false);
    }
}
