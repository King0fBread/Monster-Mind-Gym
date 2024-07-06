using UnityEditor;
using UnityEngine;

public class LevelsCollectionNameChanger : MonoBehaviour
{
    [MenuItem("Tools/Update Level Names")]
    public static void UpdateLevelNames()
    {
        string levelsCollectionPath = "Assets/LevelData/LevelsCollection.asset";
        LevelsCollection collection = AssetDatabase.LoadAssetAtPath<LevelsCollection>(levelsCollectionPath);
        if(levelsCollectionPath == null)
        {
            print("Collection not found");
            return;
        }

        for(int i = 0; i< collection.levels.Length; i++)
        {
            collection.levels[i].level = i + 1;
            EditorUtility.SetDirty(collection.levels[i]);
        }
        EditorUtility.SetDirty(collection);
        AssetDatabase.SaveAssets();
        print("renamed successfully");
    }
}
