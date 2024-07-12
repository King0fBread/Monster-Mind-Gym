using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigamesManager : MonoBehaviour
{
    [SerializeField] private GameObject _getReadyScreenObject;
    [SerializeField] private GameObject _backgroundObject;

    [SerializeField] private GameObject[] _staticUIObjectsToToggle;

    [SerializeField] private MinigameObjectAndIconPair[] _allMinigamePairs;

    [SerializeField] private TextMeshProUGUI _upcomingGameText;

    [SerializeField] private GameObject _startMinigameObject;

    [System.Serializable]
    public class MinigameObjectAndIconPair
    {
        public GameObject minigameObject;
        public MinigameIcon minigameIcon;
    }

    private int _unlockedMinigamesID;

    private List<GameObject> _playableMinigames;

    private GameObject _currentMinigame;
    private void Awake()
    {
        _startMinigameObject.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        _startMinigameObject.GetComponentInChildren<Button>().onClick.AddListener(TrySpendEnergyAndStartMinigame);

        _playableMinigames = new List<GameObject>();

        _unlockedMinigamesID = PlayerPrefs.GetInt("UnlockedMinigamesID", 0);

    }
    private void Start()
    {
        CalculateUnlockedMinigames();
    }
    private void CalculateUnlockedMinigames()
    {
        for(int i = 0; i <= _allMinigamePairs.Length-1; i++)
        {
            if(_unlockedMinigamesID >= i)
            {
                _allMinigamePairs[i].minigameIcon.UnlockIcon();

                _playableMinigames.Add(_allMinigamePairs[i].minigameObject);
            }
        }
    }
    private void InitiateRandomMinigame()
    {
        _currentMinigame = _playableMinigames[Random.Range(0, _playableMinigames.Count)];

        SetCurrentMinigameInstance();
    }
    private void SetCurrentMinigameInstance()
    {
        _startMinigameObject.SetActive(false);

        _backgroundObject.SetActive(true);
        _getReadyScreenObject.SetActive(true);

        ToggleStaticUIObjects(false);

        _upcomingGameText.text = "Upcoming game: " + _currentMinigame.name;

        _getReadyScreenObject.GetComponent<DisableSelfOrSpecified>().objectToToggle = _currentMinigame;
    }
    public void ToggleStaticUIObjects(bool activeState)
    {
        foreach(var obj in _staticUIObjectsToToggle)
        {
            obj.SetActive(activeState);
        }
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
        ToggleStaticUIObjects(true);

        _currentMinigame = null;
        _currentMinigame.SetActive(false);
    }
    public void UnlockNextMinigame()
    {
        _unlockedMinigamesID++;
        PlayerPrefs.SetInt("UnlockedMinigamesID", _unlockedMinigamesID);

        CalculateUnlockedMinigames();

    }
    public void ResetUnlockedMinigamesDEVELOPMENT()
    {
        _unlockedMinigamesID = 0;
        PlayerPrefs.SetInt("UnlockedMinigamesID", _unlockedMinigamesID);
    }
}
