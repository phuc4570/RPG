using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(UIViewId))]
public class UIViewIdDrawer : PropertyDrawer
{
    private const float ButtonWidth = 20;
    private const float Spacing = 4;
    private const float Padding = 6;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float line = EditorGUIUtility.singleLineHeight;
        return Padding * 2 + line + Spacing + line;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        GUI.Box(position, GUIContent.none, EditorStyles.helpBox);

        Rect contentRect = new Rect(
            position.x + Padding,
            position.y + Padding,
            position.width - Padding * 2,
            position.height - Padding * 2
        );

        float line = EditorGUIUtility.singleLineHeight;
        var db = FindDatabase();

        // ===== LINE 1 =====
        EditorGUI.LabelField(
            new Rect(contentRect.x, contentRect.y, EditorGUIUtility.labelWidth, line),
            label
        );

        float labelsX = contentRect.x + EditorGUIUtility.labelWidth + ButtonWidth + Spacing;
        float labelsWidth = contentRect.width - (labelsX - contentRect.x);
        float half = labelsWidth * 0.5f;

        EditorGUI.LabelField(new Rect(labelsX, contentRect.y, half - Spacing, line), "Category");
        EditorGUI.LabelField(new Rect(labelsX + half, contentRect.y, half, line), "Name");

        // ===== LINE 2 =====
        float y2 = contentRect.y + line + Spacing;

        //Rect buttonRect = new Rect(
        //    contentRect.x + EditorGUIUtility.labelWidth,
        //    y2,
        //    ButtonWidth,
        //    line
        //);

        //if (GUI.Button(buttonRect, "✚") && db != null)
        //    ViewIdEditorWindow.Open(db);

        float fieldsX = contentRect.x + EditorGUIUtility.labelWidth + ButtonWidth + Spacing;
        float fieldsWidth = contentRect.width - (fieldsX - contentRect.x);
        float halfField = fieldsWidth * 0.5f;

        Rect categoryRect = new Rect(fieldsX, y2, halfField - Spacing, line);
        Rect nameRect = new Rect(fieldsX + halfField, y2, halfField, line);

        var categoryProp = property.FindPropertyRelative("category");
        var nameProp = property.FindPropertyRelative("name");

        if (db == null || db.categories.Count == 0)
        {
            EditorGUI.LabelField(nameRect, "No Database");
            EditorGUI.EndProperty();
            return;
        }

        // ================= CATEGORY =================
        ViewCategory currentCategory = (ViewCategory)categoryProp.enumValueIndex;
        EditorGUI.BeginChangeCheck();
        ViewCategory newCategory = (ViewCategory)EditorGUI.EnumPopup(categoryRect, currentCategory);

        bool categoryChanged = false;
        bool nameChanged = false;

        if (EditorGUI.EndChangeCheck())
        {
            categoryProp.enumValueIndex = (int)newCategory;
            categoryChanged = true;
        }

        // ================= VIEW ENUM =================
        Type viewEnumType = GetViewEnumType(categoryProp.enumValueIndex);

        if (viewEnumType == null)
        {
            EditorGUI.LabelField(nameRect, "No View Enum");
        }
        else
        {
            string[] enumNames = Enum.GetNames(viewEnumType);

            if (enumNames.Length == 0)
            {
                EditorGUI.LabelField(nameRect, "Empty Enum");
            }
            else
            {
                // reset name khi đổi category hoặc name invalid
                if (categoryChanged ||
                    string.IsNullOrEmpty(nameProp.stringValue) ||
                    !enumNames.Contains(nameProp.stringValue))
                {
                    nameProp.stringValue = enumNames[0];
                    GUI.changed = true;
                }

                Enum currentEnum = (Enum)Enum.Parse(
                    viewEnumType,
                    nameProp.stringValue
                );

                Enum newEnum = EditorGUI.EnumPopup(nameRect, currentEnum);
                if (!newEnum.Equals(currentEnum))
                {
                    nameProp.stringValue = newEnum.ToString();
                    nameChanged = true;
                }
            }
        }

        if ((categoryChanged || nameChanged) &&
            property.serializedObject.targetObject is Component comp)
        {
            string categoryName = ((ViewCategory)categoryProp.enumValueIndex).ToString();
            string name = nameProp.stringValue.Replace(categoryName, string.Empty);
            name = $"{name}{categoryName}";
            comp.gameObject.name = name;
            EditorUtility.SetDirty(comp.gameObject);
        }
        EditorGUI.EndProperty();
    }

    // ================= HELPERS =================

    public static UIViewIdDatabase FindDatabase()
    {
        string[] guids = AssetDatabase.FindAssets("t:UIViewIdDatabase");
        if (guids.Length == 0) return null;

        return AssetDatabase.LoadAssetAtPath<UIViewIdDatabase>(
            AssetDatabase.GUIDToAssetPath(guids[0]));
    }

    public static Type GetViewEnumType(int categoryIndex)
    {
        string enumTypeName = $"{(ViewCategory)categoryIndex}Define";

        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            var type = asm.GetType(enumTypeName);
            if (type != null && type.IsEnum)
                return type;
        }

        return null;
    }
}
