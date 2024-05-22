using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _minigameObjects;
    private GameObject _currentMinigame;
    public void InitiateRandomMinigame()
    {
        _currentMinigame = _minigameObjects[Random.Range(0, _minigameObjects.Length)];
    }
}
