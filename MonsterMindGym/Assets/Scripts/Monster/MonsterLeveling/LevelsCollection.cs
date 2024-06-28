using UnityEngine;

[CreateAssetMenu(fileName = "LevelsCollection", menuName = "ScriptableObjects/LevelsCollection", order = 2)]
public class LevelsCollection : ScriptableObject
{
    public LevelData[] levels;
}
