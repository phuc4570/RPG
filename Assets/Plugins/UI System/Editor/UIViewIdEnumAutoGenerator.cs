using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

[InitializeOnLoad]
public static class UIViewIdEnumAutoGenerator
{
    private const string outputDir = "Assets/Plugins/UI System/Generated";

    public static void Generate()
    {
        var db = UIViewIdDrawer.FindDatabase();
        if(db == null) return;

        Directory.CreateDirectory(outputDir);

        var sb = new StringBuilder();

        sb.AppendLine("// AUTO-GENERATED - DO NOT EDIT");
        sb.AppendLine();


        // ===== Per-category views =====
        foreach (var cat in db.categories)
        {
            var enumName = $"{Sanitize(cat.name.ToString())}Define";

            sb.AppendLine($"public enum {enumName}");
            sb.AppendLine("{");

            foreach (var view in cat.views)
                sb.AppendLine($"    {Sanitize(view)},");

            sb.AppendLine("}");
            sb.AppendLine();
        }
        string filePath = $"{outputDir}/ViewIdEnums.cs";
        string newContent = sb.ToString();

        if (!File.Exists(filePath) || File.ReadAllText(filePath) != newContent)
        {
            File.WriteAllText(filePath, newContent);
            AssetDatabase.Refresh();
            Debug.Log("ViewId enums regenerated");
        }
    }

    private static string Sanitize(string s)
    {
        return s.Replace(" ", "")
                .Replace("-", "")
                .Replace("@","");
    }
}
