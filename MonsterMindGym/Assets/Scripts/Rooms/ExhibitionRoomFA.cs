using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhibitionRoomFA : MonoBehaviour, IFunctionalArea
{
    [SerializeField] private GameObject _growMonsterObject;
    public void ExecuteMechanicOnAreaEntrance()
    {
        //check if conditions are met
        _growMonsterObject.SetActive(true);
    }
    public void ExecuteMechanicOnAreaExit()
    {
        _growMonsterObject.SetActive(false);
        
    }
}
