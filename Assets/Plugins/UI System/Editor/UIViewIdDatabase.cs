using UnityEngine;
using System.Collections.Generic;
using System;

public class UIViewIdDatabase : ScriptableObject
{
    public List<CategoryEntry> categories = new List<CategoryEntry>();

    [Serializable]
    public class CategoryEntry
    {
        public ViewCategory name;
        public List<string> views = new();
    }
}
