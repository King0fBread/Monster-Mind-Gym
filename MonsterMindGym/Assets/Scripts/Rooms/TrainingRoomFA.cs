using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingRoomFA : MonoBehaviour, IFunctionalArea
{
    [SerializeField] private Button _startMinigameButton;
    public void ExecuteMechanicOnAreaEntrance()
    {
        _startMinigameButton.gameObject.SetActive(true);
    }
    public void ExecuteMechanicOnAreaExit()
    {
        _startMinigameButton.gameObject.SetActive(false);
    }
}
