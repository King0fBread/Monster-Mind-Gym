using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCapsuleUpdater : MonoBehaviour
{
    [SerializeField] private GameObject _normalCapsule;
    [SerializeField] private GameObject _brokenCapsule;

    public void SwitchToNormalCapsule()
    {
        _brokenCapsule.SetActive(false);
        _normalCapsule.SetActive(true);
    }
    public void SwitchToBrokenCapsule()
    {
        _normalCapsule.SetActive(false);
        _brokenCapsule.SetActive(true);
    }
    public void RemoveCapsule()
    {
        _normalCapsule.SetActive(false);
        _brokenCapsule.SetActive(false);
    }
}
