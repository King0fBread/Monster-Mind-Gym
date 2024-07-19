using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingRoomFA : MonoBehaviour, IFunctionalArea
{
    [SerializeField] private GameObject _startMinigameObject;
    [SerializeField] private GameObject _postGameBonus;
    public void ExecuteMechanicOnAreaEntrance()
    {
        _startMinigameObject.SetActive(true);
    }
    public void ExecuteMechanicOnAreaExit()
    {
        _startMinigameObject.SetActive(false);
        _postGameBonus.SetActive(false);
    }
}
