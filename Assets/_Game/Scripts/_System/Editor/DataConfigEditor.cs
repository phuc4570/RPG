using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class DataConfigEditor : OdinMenuEditorWindow
{
    //private string path = "Assets/_Game/Resources/ScriptableObject/";

    [MenuItem("TapHoaVuiVe/Data Config")]
    private static void OpenWindow()
    {
        var window = GetWindow<DataConfigEditor>();
        window.titleContent = new GUIContent("Data Config");
        window.Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(true);

        //var faceConfig = CreateInstance<FaceConfigEditor>();
        //faceConfig.Initialize($"{path}FaceConfig.asset");

        //tree.Add("Face Config", faceConfig);

        return tree;
    }

    //public class FaceConfigEditor : ScriptableObjectNode<FaceConfig> { }

}
