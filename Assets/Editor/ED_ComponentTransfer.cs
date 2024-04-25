using UnityEngine;
using UnityEditor;

public class ED_ComponentTransfer : EditorWindow
{
    private GameObject sourceObject;
    private GameObject targetObject;

    [MenuItem("Custom/Component Transfer")]
    static void Init()
    {
        ED_ComponentTransfer window = (ED_ComponentTransfer)EditorWindow.GetWindow(typeof(ED_ComponentTransfer));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Component Transfer", EditorStyles.boldLabel);

        sourceObject = EditorGUILayout.ObjectField("Source Object", sourceObject, typeof(GameObject), true) as GameObject;
        targetObject = EditorGUILayout.ObjectField("Target Object", targetObject, typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("Transfer Components"))
        {
            TransferComponents();
        }
    }

    void TransferComponents()
    {
        if (sourceObject == null || targetObject == null)
        {
            Debug.LogError("Source or target object is null!");
            return;
        }

        Component[] componentsToTransfer = sourceObject.GetComponents<Component>();

        foreach (Component component in componentsToTransfer)
        {
            if (component.GetType() != typeof(Transform)) // Exclude Transform component
            {
                UnityEditorInternal.ComponentUtility.CopyComponent(component);
                UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetObject);
            }
        }

        Debug.Log("Components transferred successfully!");
    }
}
