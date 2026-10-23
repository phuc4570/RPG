using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class DataConfigEditor : OdinMenuEditorWindow
{
    private string path = "Assets/_Game/Resources/ScriptableObject/";

    [MenuItem("RPG/Data Config")]
    private static void OpenWindow()
    {
        var window = GetWindow<DataConfigEditor>();
        window.titleContent = new GUIContent("Data Config");
        window.Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(true);

        var enemyConfig = CreateInstance<EnemyConfigEditor>();
        enemyConfig.Initialize($"{path}EnemyConfig.asset");

        var bossConfig = CreateInstance<BossConfigEditor>();
        bossConfig.Initialize($"{path}BossConfig.asset");

        tree.Add("Enemy Config", enemyConfig);
        tree.Add("Boss Config", bossConfig);

        return tree;
    }

    public class EnemyConfigEditor : ScriptableObjectNode<EnemyConfig> { }
    public class BossConfigEditor : ScriptableObjectNode<BossConfig> { }

}
