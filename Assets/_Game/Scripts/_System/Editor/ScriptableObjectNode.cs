using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

//Script để handle việc vẽ data ScriptableObject lên Odin
public class ScriptableObjectNode<T> : ScriptableObject where T : ScriptableObject
{
    //Hiển thị data lên cửa sổ Odin, ẩn phần object field đi
    [InlineEditor(InlineEditorModes.GUIOnly,ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
    public T Ins;

    //Hàm khởi tạo class, kiểm tra nếu Scriptable có sẵn thì load lên, không thì tạo một cái mới dựa theo path được truyền vào
    public void Initialize(string path)
    {
        Ins = AssetDatabase.LoadAssetAtPath<T>(path);
        if(Ins == null)
        {
            Ins = CreateInstance<T>();

            AssetDatabase.CreateAsset(Ins, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
