using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitionRoomFA : MonoBehaviour, IFunctionalArea
{
    [SerializeField] private Button _growMonsterButton;
    public void ExecuteMechanicOnAreaEntrance()
    {
        //check if conditions are met
        _growMonsterButton.gameObject.SetActive(true);
    }
    public void ExecuteMechanicOnAreaExit()
    {
        _growMonsterButton.gameObject.SetActive(false);
        
    }
}
