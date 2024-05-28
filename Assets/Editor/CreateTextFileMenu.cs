using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateTextFileMenu : EditorWindow
{
    [MenuItem("Assets/Create/Text File", false, 2)]
    static void CreateTextFile()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string fileName = "New Text File.txt";
        string fullPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(path, fileName));
        
        try
        {
            File.WriteAllText(fullPath, "");
            AssetDatabase.Refresh();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath(fullPath, typeof(Object));
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to create text file: " + e.Message);
        }
    }
}
