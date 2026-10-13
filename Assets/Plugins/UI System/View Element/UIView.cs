using UnityEngine;
using System;

public class UIView : MonoBehaviour
{
    [SerializeField] internal UIViewId viewId;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private ShowHideExecute showAction;
    [SerializeField] private ShowHideExecute hideAction;
    private UIViewAnimator uiViewAnimator;
    public UIViewAnimator Animator 
    {
        get
        {
            if (uiViewAnimator == null) uiViewAnimator = GetComponent<UIViewAnimator>();
            return uiViewAnimator;
        }
    }

    private static bool isShowing = false;
    public static bool IsShowing => isShowing;

    public bool destroyWhenHide;

    protected object[] @params;
    public Action<UIView> onShow, onHide, onShowFinished, onHideFinished;

    public void UIShow(object[] @params = null) 
    { 
        isShowing = true;
        canvasGroup.interactable = true;
        this.@params = @params;
        onShow?.Invoke(this);

        gameObject.SetActive(true);
        OnShow();

        switch (showAction)
        {
            case ShowHideExecute.Animation:
                Animator.Show(ShowFinished);
                break;
            case ShowHideExecute.Instant:
                Animator.InstantShow(ShowFinished);
                break;
        }
    }

    public void Hide()
    {
        isShowing = false;
        canvasGroup.interactable = false;
        onHide?.Invoke(this);
        OnHide();

        switch (hideAction)
        {
            case ShowHideExecute.Animation:
                Animator.Hide(HideFinished);
                break;
            case ShowHideExecute.Instant:
                Animator.InstantHide(HideFinished);
                break;
        }
    }
    private void ShowFinished() 
    {
        OnShowFinished();
        onShowFinished?.Invoke(this);
    }

    private void HideFinished() 
    {
        OnHideFinished();

        gameObject.SetActive(false);
        onHideFinished?.Invoke(this);
    }

    protected virtual void OnShow() { }

    protected virtual void OnShowFinished() { }

    protected virtual void OnHide() { }

    protected virtual void OnHideFinished() { }

    private void OnValidate()
    {
        if(canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
    }

    public enum ShowHideExecute
    {
        Animation,
        Instant,
    }
}