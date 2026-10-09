using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class UIViewAnimator : MonoBehaviour
{
    #region Config
    public UiAnim showAnim;
    public UiAnim hideAnim;
    #endregion

    #region Runtime refs
    private RectTransform rect;
    private CanvasGroup canvas;
    private LayoutGroup layoutGroup;

    private RectTransform Rect => rect ? rect : rect = GetComponent<RectTransform>();
    private CanvasGroup Canvas => canvas ? canvas : canvas = GetComponent<CanvasGroup>();
    private LayoutGroup LayoutGroup => layoutGroup ? layoutGroup : layoutGroup = GetComponentInParent<LayoutGroup>();
    #endregion

    #region Tweens
    private Tween moveTween;
    private Tween scaleTween;
    private Tween fadeTween;
    private Tween rotateTween;
    #endregion

    private bool isInLayoutGroup;
    private bool needLateLayoutSync;

    public UIAnimState State { get; private set; } = UIAnimState.Hidden;

    #region Unity Message
    private void Awake()
    {
        DetectLayout();
        State = UIAnimState.Hidden;
    }

    private void LateUpdate()
    {
        if (!needLateLayoutSync) return;
        if (!isInLayoutGroup) return;
        if (IsAnyTweenPlaying()) return;
        if (CanvasUpdateRegistry.IsRebuildingLayout()) return;

        ForceLayoutSync();
        needLateLayoutSync = false;
    }

    private void OnDisable()
    {
        KillTweens();
    }
    #endregion

    #region Public API
    public void Show(System.Action onFinished = null)
    {
        Play(showAnim, UIAnimState.Showing, UIAnimState.Visible, onFinished);
    }

    public void Hide(System.Action onFinished = null)
    {
        Play(hideAnim, UIAnimState.Hiding, UIAnimState.Hidden, onFinished);
    }

    public void Reverse()
    {
        moveTween?.PlayBackwards();
        scaleTween?.PlayBackwards();
        fadeTween?.PlayBackwards();
        rotateTween?.PlayBackwards();

        State = State == UIAnimState.Showing
            ? UIAnimState.Hiding
            : UIAnimState.Showing;
    }

    public void InstantShow(System.Action onFinished = null)
    {
        KillTweens();
        ApplyInstant(showAnim);
        State = UIAnimState.Visible;

        if (isInLayoutGroup)
            needLateLayoutSync = true;

        onFinished?.Invoke();
    }

    public void InstantHide(System.Action onFinished = null)
    {
        KillTweens();
        ApplyInstant(hideAnim);
        State = UIAnimState.Hidden;

        onFinished?.Invoke();
    }
    #endregion

    #region Core
    private void Play(UiAnim anim, UIAnimState playing, UIAnimState completed, System.Action onFinished)
    {
        KillTweens();
        State = playing;

        moveTween = PlayMove(anim.move);
        scaleTween = PlayScale(anim.scale);
        fadeTween = PlayFade(anim.fade);
        rotateTween = PlayRotate(anim.rotate);

        // completion watcher (không chứa tween)
        float maxDuration = anim.MaxDuration();
        if (maxDuration > 0f)
        {
            DOVirtual.DelayedCall(maxDuration, () =>
            {
                State = completed;
                if (completed == UIAnimState.Visible && isInLayoutGroup)
                    needLateLayoutSync = true;

                onFinished?.Invoke();
            }).SetUpdate(true);
        }
        else
        {
            State = completed;
            onFinished?.Invoke();
        }
    }
    #endregion

    #region Tween
    private Tween PlayMove(UiAnimSetting s)
    {
        if (!s.enabled || s.durationAnim <= 0f) return null;

        Rect.anchoredPosition3D = s.customStart;

        var t = Rect.DOAnchorPos3D(s.customEnd, s.durationAnim)
            .SetEase(s.ease)
            .SetUpdate(true);

        ApplyLoopAndDelay(t, s);
        return t;
    }

    private Tween PlayScale(UiAnimSetting s)
    {
        if (!s.enabled || s.durationAnim <= 0f) return null;

        Rect.localScale = s.customStart;

        var t = Rect.DOScale(s.customEnd, s.durationAnim)
            .SetEase(s.ease)
            .SetUpdate(true);

        ApplyLoopAndDelay(t, s);
        return t;
    }

    private Tween PlayFade(UiAnimSetting s)
    {
        if (!s.enabled || s.durationAnim <= 0f) return null;

        Canvas.alpha = s.customStart.z;

        var t = Canvas.DOFade(s.customEnd.z, s.durationAnim)
            .SetEase(s.ease)
            .SetUpdate(true);

        ApplyLoopAndDelay(t, s);
        return t;
    }

    private Tween PlayRotate(UiAnimSetting s)
    {
        if (!s.enabled || s.durationAnim <= 0f) return null;

        Rect.localEulerAngles = s.customStart;

        var t = Rect.DOLocalRotate(s.customEnd, s.durationAnim)
            .SetEase(s.ease)
            .SetUpdate(true);

        ApplyLoopAndDelay(t, s);
        return t;
    }
    #endregion

    #region Instant
    private void ApplyInstant(UiAnim anim)
    {
        ApplyInstant(anim.move);
        ApplyInstant(anim.scale);
        ApplyInstant(anim.fade);
        ApplyInstant(anim.rotate);
    }

    private void ApplyInstant(UiAnimSetting s)
    {
        if (!s.enabled) return;

        Rect.anchoredPosition3D = s.customStart;
        Rect.localScale = s.customStart;
        Rect.localEulerAngles = s.customStart;
        Canvas.alpha = s.customStart.z;
    }
    #endregion

    #region Helpers
    private void ApplyLoopAndDelay(Tween t, UiAnimSetting s)
    {
        if (s.loopCount != 0)
        {
            t.SetLoops(
                s.loopCount < 0 ? -1 : s.loopCount,
                s.loopType
            );
        }

        if(s.delay > 0)
        {
            t.SetDelay(s.delay);
        }
    }

    private bool IsAnyTweenPlaying()
    {
        return (moveTween?.IsActive() ?? false)
            || (scaleTween?.IsActive() ?? false)
            || (fadeTween?.IsActive() ?? false)
            || (rotateTween?.IsActive() ?? false);
    }

    public void KillTweens()
    {
        moveTween?.Kill(true);
        scaleTween?.Kill(true);
        fadeTween?.Kill(true);
        rotateTween?.Kill(true);

        moveTween = scaleTween = fadeTween = rotateTween = null;
    }

    private void DetectLayout()
    {
        isInLayoutGroup = LayoutGroup != null;
    }

    private void ForceLayoutSync()
    {
        if (LayoutGroup == null) return;
        LayoutRebuilder.ForceRebuildLayoutImmediate(
            LayoutGroup.GetComponent<RectTransform>()
        );
    }
    #endregion

    #region Data
    [System.Serializable]
    public struct UiAnim
    {
        public UiAnimSetting move;
        public UiAnimSetting scale;
        public UiAnimSetting fade;
        public UiAnimSetting rotate;

        public float MaxDuration()
        {
            float d = 0f;
            if (move.enabled) d = Mathf.Max(d, move.durationAnim);
            if (scale.enabled) d = Mathf.Max(d, scale.durationAnim);
            if (fade.enabled) d = Mathf.Max(d, fade.durationAnim);
            if (rotate.enabled) d = Mathf.Max(d, rotate.durationAnim);
            return d;
        }
    }

    [System.Serializable]
    public struct UiAnimSetting
    {
        public bool enabled;
        public float durationAnim;
        public Ease ease;
        public Vector3 customStart;
        public Vector3 customEnd;
        public int loopCount;     // -1 = infinite
        public LoopType loopType;
        public float delay;
    }

    public enum UIAnimState
    {
        Hidden,
        Showing,
        Visible,
        Hiding
    }
    #endregion

#if UNITY_EDITOR
    public void EditorPreviewShow() => Show();
    public void EditorPreviewHide() => Hide();
#endif
}
