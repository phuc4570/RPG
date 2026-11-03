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

        var visualStatusConfig = CreateInstance<VisualStatusConfigEditor>();
        visualStatusConfig.Initialize($"{path}VisualStatusConfig.asset");

        var vfxConfig = CreateInstance<VFXConfigEditor>();
        vfxConfig.Initialize($"{path}VFXConfig.asset");

        var soundConfig = CreateInstance<SoundConfigEditor>();
        soundConfig.Initialize($"{path}SoundConfig.asset");

        tree.Add("Enemy Config", enemyConfig);
        tree.Add("Boss Config", bossConfig);
        tree.Add("Visual Status Config", visualStatusConfig);
        tree.Add("VFX Config", vfxConfig);
        tree.Add("Sound Config", soundConfig);

        return tree;
    }

    public class EnemyConfigEditor : ScriptableObjectNode<EnemyConfig> { }
    public class BossConfigEditor : ScriptableObjectNode<BossConfig> { }
    public class VisualStatusConfigEditor : ScriptableObjectNode<VisualStatusConfig> { }
    public class VFXConfigEditor : ScriptableObjectNode<VFXConfig> { }
    public class SoundConfigEditor : ScriptableObjectNode<SoundConfig> { }

}
