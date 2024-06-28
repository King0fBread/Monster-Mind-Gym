using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int level;
    public int upgradeCost;

    public int maxEnergyIncrease;
    public float energyRecoveryDecrease;

    public bool unlockNextMinigame;
    public bool increasePostMinigameBonus;

    public bool visuallyUpgradeMonster;
}
