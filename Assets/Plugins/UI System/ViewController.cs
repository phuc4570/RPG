using UnityEngine;
using System.Collections.Generic;

public class ViewController : SingletonMono<ViewController>
{
    [SerializeField] private ViewDefine firstViewOpen;
    [SerializeField] private string resourcePath;
    private UIView currentView;

    private Dictionary<string, UIView> dicView = new();

    protected override void Start()
    {
        base.Start();
        if(firstViewOpen != ViewDefine.None ) {
            Show(firstViewOpen);
        }
    }

    public UIView Show(ViewDefine viewDefine, params object[] @params)
    {
        string viewName = viewDefine.ToString();
        var view = ValidateView(viewName);

        if (view == null || currentView == view) return view;

        if(currentView)
        {
            if (currentView.destroyWhenHide)
            {
                currentView.onHideFinished -= DeleteView;
                currentView.onHideFinished += DeleteView;
            }
            currentView.Hide();
        }

        view.UIShow(@params);
        currentView = view;
        return view;
    }

    private void DeleteView(UIView deleteView)
    {
        if (deleteView == null) return;

        deleteView.onHideFinished -= DeleteView;
        dicView.Remove(deleteView.viewId.name);
        Destroy(deleteView.gameObject);
    }

    private UIView ValidateView(string viewName)
    {
        if(!dicView.TryGetValue(viewName, out UIView view) || view == null)
        {
            string finalName = viewName;
            string requiredSuffix = ViewCategory.View.ToString();
            if (!viewName.EndsWith(requiredSuffix))
                finalName += requiredSuffix;

            string path = $"{resourcePath}/{finalName}";
            var sample = Resources.Load<UIView>(path);
            if (sample == null)
            {
                Debug.LogError($"View not found at Resources/{path}");
                return null;
            }
            view = Instantiate(sample, transform, false);
            dicView[viewName] = view;

            return view;
        }

        return view;
    }
}
