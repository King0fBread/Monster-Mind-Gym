using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElixirsManager : MonoBehaviour
{
    [SerializeField] private GameObject _adblockObject;
    private void OnEnable()
    {
        _adblockObject.SetActive(true);
    }
}
