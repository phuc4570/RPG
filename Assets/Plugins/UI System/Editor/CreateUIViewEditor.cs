using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using UnityEditor.Callbacks;
#if UNITY_2018
using System.Reflection;
#endif

public class CreateUIViewEditor : Editor
{
    public const string viewSample = "View Sample";
    public const string viewTemplate = "ViewTemplate";
    public const string dialogTemplate = "DialogTemplate";
    private const string PendingKey = "CREATE_VIEW_PENDING";
    private const string PendingParentKey = "PARENT_VIEW_PENDING";
    private const string PendingCategoryKey = "VIEW_CATEGORY_PENDING";
    private const string LastSaveViewCategoryPathKey = "PATH_SAVE_VIEW_CATEGORY";
    private const string LastSaveDialogCategoryPathKey = "PATH_SAVE_DIALOG_CATEGORY";

    [MenuItem("GameObject/Undefine/Create/View", false, 0)]
    private static void CreateView(MenuCommand menuCommand)
    {
        GameObject selected = Selection.activeGameObject;
        int parentId = selected != null ? selected.GetInstanceID() : 0;

        string scriptPath = GetDestinationPath("NewView", ViewCategory.View);
        if (string.IsNullOrEmpty(scriptPath))
            return;

        string scriptName = Path.GetFileNameWithoutExtension(scriptPath);

        CreateScript(ViewCategory.View, scriptPath);

        AddToViewDatabase(scriptName, ViewCategory.View);

        SessionState.SetString(PendingKey, scriptName);
        SessionState.SetInt(PendingParentKey, parentId);
        SessionState.SetInt(PendingCategoryKey, (int)ViewCategory.View);
    }

    [MenuItem("GameObject/Undefine/Create/Dialog", false, 0)]
    private static void CreateDialog(MenuCommand menuCommand)
    {
        GameObject selected = Selection.activeGameObject;
        int parentId = selected != null ? selected.GetInstanceID() : 0;

        string scriptPath = GetDestinationPath("NewDialog", ViewCategory.Dialog);
        if (string.IsNullOrEmpty(scriptPath))
            return;

        string scriptName = Path.GetFileNameWithoutExtension(scriptPath);

        CreateScript(ViewCategory.Dialog, scriptPath);

        AddToViewDatabase(scriptName, ViewCategory.Dialog);

        SessionState.SetString(PendingKey, scriptName);
        SessionState.SetInt(PendingParentKey, parentId);
        SessionState.SetInt(PendingCategoryKey, (int)ViewCategory.Dialog);
    }

    [MenuItem("GameObject/Undefine/Delete", false, 0)]
    private static void DeleteSelected(MenuCommand menuCommand)
    {
        GameObject selected = Selection.activeGameObject;
        if (selected == null)
        {
            Debug.LogWarning("No GameObject selected");
            return;
        }

        bool confirm = EditorUtility.DisplayDialog(
           "Confirm Delete",
           $"Are you sure you want to delete '{selected.name}'?\n\n" +
           "This will:\n" +
           "- Remove it from UIViewDatabase (if exists)\n" +
           "- Delete its script file\n" +
           "- Destroy the GameObject/Prefab",
           "Delete",
           "Cancel"
        );

        if (!confirm)
            return;

        var componentWithViewId = selected.GetComponents<MonoBehaviour>()
            .FirstOrDefault(c =>
            {
                if (c == null) return false;
                var so = new SerializedObject(c);
                return so.FindProperty("viewId") != null;
            });

        if (componentWithViewId != null)
        {
            RemoveFromViewDatabase(componentWithViewId);
        }

        string scriptPath = AssetDatabase.GetAssetPath(MonoScript.FromMonoBehaviour(componentWithViewId as MonoBehaviour));
        if (!string.IsNullOrEmpty(scriptPath))
        {
            AssetDatabase.DeleteAsset(scriptPath);
        }

        GameObject prefabAsset = PrefabUtility.GetCorrespondingObjectFromSource(selected);
        if (prefabAsset != null)
        {
            string prefabPath = AssetDatabase.GetAssetPath(prefabAsset);
            if (!string.IsNullOrEmpty(prefabPath))
            {
                AssetDatabase.DeleteAsset(prefabPath);
            }
        }
        else
        {
            string assetPath = AssetDatabase.GetAssetPath(selected);
            if (!string.IsNullOrEmpty(assetPath) && assetPath.EndsWith(".prefab"))
            {
                AssetDatabase.DeleteAsset(assetPath);
            }
        }

        Undo.DestroyObjectImmediate(selected);
    }

    public static void CreateScript(ViewCategory viewCategory, string destinationPath)
    {
        string templateName = viewCategory == ViewCategory.View ? viewTemplate : dialogTemplate;
        var template = Resources.Load(templateName) as TextAsset;
        Resources.UnloadAsset(template);

        string fileName = System.IO.Path.GetFileNameWithoutExtension(destinationPath);
        string scriptName = fileName;
        string id = scriptName.Replace(viewCategory == ViewCategory.View ? ViewCategory.View.ToString() : ViewCategory.Dialog.ToString(), "");
        string content = template.text
            .Replace("#SCRIPTNAME#", fileName)
            .Replace("#ID#", id);

        System.IO.File.WriteAllText(destinationPath, content);
        AssetDatabase.Refresh();
    }

    private static string GetDestinationPath(string defaultName, ViewCategory category)
    {
        string key = category == ViewCategory.View ? LastSaveViewCategoryPathKey : LastSaveDialogCategoryPathKey;
        string lastFolder = EditorPrefs.GetString(key, "Assets");

        string path = EditorUtility.SaveFilePanelInProject(
            "Create Script",
            defaultName,
            "cs",
            "Enter file name",
            lastFolder
        );

        string folder = System.IO.Path.GetDirectoryName(path);
        string fileName = System.IO.Path.GetFileNameWithoutExtension(path);

        // ===== VALIDATE SUFFIX =====
        string requiredSuffix = category.ToString();

        if (!fileName.EndsWith(requiredSuffix))
        {
            fileName += requiredSuffix;
        }

        string finalPath = System.IO.Path.Combine(folder, fileName + ".cs");

        // Nếu user gõ tên khác với final → đổi lại
        if (finalPath != path)
        {
            path = finalPath;
        }

        if (!string.IsNullOrEmpty(path))
        {
            EditorPrefs.SetString(key, folder);
        }
        return path;
    }

    private static void AddToViewDatabase(string viewName, ViewCategory category)
    {
        var db = UIViewIdDrawer.FindDatabase();

        if (db == null)
        {
            Debug.LogError("Cannot load ViewIdDatabase");
            return;
        }

        Undo.RecordObject(db, "Add ViewId");
        var categoryData = db.categories.FirstOrDefault(c => c.name == category);

        if (categoryData == null)
        {
            Debug.LogError($"Category {category} not found in database");
            return;
        }

        var nameValidate = viewName.Replace(category.ToString(), "");
        if (!categoryData.views.Contains(nameValidate))
        {
            categoryData.views.Add(nameValidate);
            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();

            UIViewIdEnumAutoGenerator.Generate();
        }
    }

    [DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        string scriptName = SessionState.GetString(PendingKey, null);
        int categoryIndex = SessionState.GetInt(PendingCategoryKey, -1);
        if (string.IsNullOrEmpty(scriptName) || categoryIndex == -1)
            return;

        SessionState.EraseString(PendingKey);
        SessionState.EraseString(PendingCategoryKey);

        CreateGameObjectWithScript(scriptName, (ViewCategory)categoryIndex);
    }

    private static void CreateGameObjectWithScript(string scriptName, ViewCategory category)
    {
        GameObject parentObj = null;
        int parentId = SessionState.GetInt(PendingParentKey, 0);
        if (parentId != 0)
            parentObj = EditorUtility.EntityIdToObject(parentId) as GameObject;

        GameObject newObj = new GameObject(scriptName);
        newObj.transform.SetParent(parentObj ? parentObj.transform : null);
        var rectTrans = newObj.AddComponent<RectTransform>();
        rectTrans.localPosition = Vector3.zero;
        rectTrans.localScale = Vector3.one;

        rectTrans.anchorMin = Vector2.zero; // Equivalent to new Vector2(0, 0)
        rectTrans.anchorMax = Vector2.one;  // Equivalent to new Vector2(1, 1)
        rectTrans.offsetMin = Vector2.zero; // Sets Left and Bottom to 0
        rectTrans.offsetMax = Vector2.zero; // Sets Right and Top to 0

        newObj.AddComponent<CanvasGroup>();
        newObj.AddComponent<UnityEngine.UI.Image>();
        var type = System.AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.Name == scriptName);
        if (type != null && type.IsSubclassOf(typeof(MonoBehaviour)))
        {
            var component = newObj.AddComponent(type);
            SetViewId(component, category, scriptName);
        }
        newObj.AddComponent<UIViewAnimator>();

        SessionState.EraseInt(PendingParentKey);

        Undo.RegisterCreatedObjectUndo(newObj, "Create View");
        Selection.activeObject = newObj;
    }

    private static void SetViewId(Component component, ViewCategory category, string scriptName)
    {
        var so = new SerializedObject(component);
        var viewIdProp = so.FindProperty("viewId");

        if (viewIdProp == null)
        {
            Debug.LogWarning("viewId field not found on component");
            return;
        }

        var categoryProp = viewIdProp.FindPropertyRelative("category");
        var nameProp = viewIdProp.FindPropertyRelative("name");

        if (categoryProp == null || nameProp == null)
        {
            Debug.LogWarning("ViewId struct layout mismatch");
            return;
        }

        categoryProp.enumValueIndex = (int)category;
        nameProp.stringValue = scriptName.Replace(category.ToString(), "");

        so.ApplyModifiedProperties();
        EditorUtility.SetDirty(component);
    }

    private static void RemoveFromViewDatabase(Component component)
    {
        var so = new SerializedObject(component);
        var viewIdProp = so.FindProperty("viewId");

        if (viewIdProp == null)
            return;

        var categoryProp = viewIdProp.FindPropertyRelative("category");
        var nameProp = viewIdProp.FindPropertyRelative("name");

        if (categoryProp == null || nameProp == null)
            return;

        ViewCategory category = (ViewCategory)categoryProp.enumValueIndex;

        string name = nameProp.stringValue;

        if (string.IsNullOrEmpty(name))
            return;

        var db = UIViewIdDrawer.FindDatabase();
        if (db == null)
            return;

        Undo.RecordObject(db, "Remove ViewId");

        var categoryData = db.categories
            .FirstOrDefault(c => c.name == category);

        if (categoryData == null)
            return;

        if (categoryData.views.Contains(name))
        {
            categoryData.views.Remove(name);

            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();

            UIViewIdEnumAutoGenerator.Generate();

            Debug.Log($"Removed ViewId: {category}/{name}");
        }
    }

}
