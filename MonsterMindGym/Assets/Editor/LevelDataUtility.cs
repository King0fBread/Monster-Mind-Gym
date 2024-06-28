using UnityEditor;
using UnityEngine;

public class LevelDataUtility
{
    [MenuItem("Tools/Create Level Data")]
    public static void CreateLevelData()
    {
        LevelsCollection levelsCollection = ScriptableObject.CreateInstance<LevelsCollection>();
        levelsCollection.levels = new LevelData[90];

        for(int i = 0; i < 90; i++)
        {
            LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
            levelData.level = i;
            levelData.upgradeCost = 100 + (i * 100);

            if(i==1 || i==11 || i==21 || i==31)
            {
                levelData.unlockNextMinigame = true;
            }
            if (i % 10 == 4)
            {
                levelData.maxEnergyIncrease = 1;
            }
            if (i % 10 == 9)
            {
                levelData.energyRecoveryDecrease = 0.5f;
            }
            if (i % 10 == 6)
            {
                levelData.increasePostMinigameBonus = true;
            }

            AssetDatabase.CreateAsset(levelData, $"Assets/LevelData/Level_{i + 1}.asset");
            levelsCollection.levels[i] = levelData;
        }
        AssetDatabase.CreateAsset(levelsCollection, "Assets/LevelData/LevelsCollection.asset");
        AssetDatabase.SaveAssets();
    }
}
