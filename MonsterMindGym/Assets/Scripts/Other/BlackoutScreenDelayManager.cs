using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutScreenDelayManager : MonoBehaviour
{
    [SerializeField] private GameObject _blackScreenObject;

    private static BlackoutScreenDelayManager _instance;
    public static BlackoutScreenDelayManager Instance { get { return _instance; } }
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void InitiateBlackoutDelay()
    {
        _blackScreenObject.SetActive(true);
    }

}
