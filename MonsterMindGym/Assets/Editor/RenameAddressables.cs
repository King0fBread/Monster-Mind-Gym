using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;

public class RenameAddressables : MonoBehaviour
{
    [MenuItem("Tools/Rename Addressable Assets")]
    public static void RenameAdressableAssets()
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
        if (settings == null)
        {
            return;
        }
        
        foreach(var group in settings.groups)
        {
            foreach(var entry in group.entries)
            {
                string path = AssetDatabase.GUIDToAssetPath(entry.guid);

                string fileName = System.IO.Path.GetFileNameWithoutExtension(path);

                entry.SetAddress(fileName);
            }
        }
        AssetDatabase.SaveAssets();

        print("Renamed addressables to their direct file names");
    }
}
