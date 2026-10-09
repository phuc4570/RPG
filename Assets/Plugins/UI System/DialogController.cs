using System.Collections.Generic;
using UnityEngine;

public class DialogController : SingletonMono<DialogController>
{
    [SerializeField] private string resourcePath;
    private Dictionary<string, UIView> dicDialog = new Dictionary<string, UIView>();
    
    public UIView Show(DialogDefine dialogDefine, params object[] @params)
    {
        string dialogName = dialogDefine.ToString();
        var dialog = ValidateView(dialogName);

        if (dialog != null)
        {
            dialog.transform.SetAsLastSibling();
            dialog.UIShow(@params);
        }

        return dialog;
    }

    public void Hide(DialogDefine dialogDefine)
    {
        string dialogName = dialogDefine.ToString();
        if (dicDialog.TryGetValue(dialogName, out var dialog))
        {
            if (dialog)
            {
                if (dialog.destroyWhenHide)
                {
                    dialog.onHideFinished -= DeleteDialog;
                    dialog.onHideFinished += DeleteDialog;
                }
                dialog.Hide();
            }
        }
    }

    private void DeleteDialog(UIView deleteView)
    {
        if (deleteView == null) return;

        deleteView.onHideFinished -= DeleteDialog;
        dicDialog.Remove(deleteView.viewId.FullName);
        Destroy(deleteView.gameObject);
    }

    private UIView ValidateView(string dialogName)
    {
        if (!dicDialog.TryGetValue(dialogName, out UIView dialog) || dialog == null)
        {
            string finalName = dialogName;
            string requiredSuffix = ViewCategory.Dialog.ToString();
            if (!dialogName.EndsWith(requiredSuffix))
                finalName += requiredSuffix;

            string path = System.IO.Path.Combine(resourcePath, finalName);
            var sample = Resources.Load<UIView>(path);
            if (sample == null)
            {
                Debug.LogError($"View not found at Resources/{path}");
                return null;
            }
            dialog = Instantiate(sample, transform, false);
            dicDialog[dialogName] = dialog;

            return dialog;
        }

        return dialog;
    }
}
