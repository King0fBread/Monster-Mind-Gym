using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingRoomFA : MonoBehaviour, IFunctionalArea
{
    [SerializeField] private GameObject _startMinigameObject;
    public void ExecuteMechanicOnAreaEntrance()
    {
        _startMinigameObject.SetActive(true);
    }
    public void ExecuteMechanicOnAreaExit()
    {
        _startMinigameObject.SetActive(false);
    }
}
