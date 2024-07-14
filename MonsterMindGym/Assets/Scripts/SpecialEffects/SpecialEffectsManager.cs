using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpecialEffectsManager : MonoBehaviour
{
    [Header("Monster Upgrade Effects")]
    [SerializeField] private GameObject _monsterVisualUpgradeSpecialEffect;
    [SerializeField] private GameObject _monsterBasicUpgradeSpecialEffect;

    private static SpecialEffectsManager _instance;
    public static SpecialEffectsManager Instance { get { return _instance; } }
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
    public void DisplayMonsterVisualUpgradeEffect()
    {
        _monsterVisualUpgradeSpecialEffect.SetActive(true);
    }
    public void DisplayMonsterBasicUpgradeEffect()
    {
        _monsterBasicUpgradeSpecialEffect.SetActive(true);
    }
}