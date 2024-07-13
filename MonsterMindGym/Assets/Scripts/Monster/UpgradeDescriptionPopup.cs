using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeDescriptionPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _upgradeDescriptionText;
    public void DisplayUpgradePopup(LevelData currentLevelData)
    {
        if (currentLevelData.visuallyUpgradeMonster)
        {
            EnablePopup("MONSTER EVOLUTION\n\n+Energy recovery");
        }
        if (currentLevelData.unlockNextMinigame)
        {
            EnablePopup("Unlocked new minigame");
        }
        if (currentLevelData.energyRecoveryDecrease > 0 && !currentLevelData.visuallyUpgradeMonster)
        {
            EnablePopup("+Energy recovery");
        }
        if (currentLevelData.maxEnergyIncrease > 0)
        {
            EnablePopup("+Max energy");
        }
        if (currentLevelData.increasePostMinigameBonus)
        {
            EnablePopup("+Minigame bonus amount");
        }
    }
    private void EnablePopup(string text)
    {
        _upgradeDescriptionText.gameObject.SetActive(true);
        _upgradeDescriptionText.text = text;
    }
}
