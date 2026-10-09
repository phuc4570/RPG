[System.Serializable]
public struct UIViewId
{
    public ViewCategory category;
    public string name;

    public string FullName
    {
        get
        {
            string cate = category.ToString();
            return $"{name.Replace(cate, "")}{cate}";
        }
    }
}

public enum ViewCategory
{
    View,
    Dialog,
}