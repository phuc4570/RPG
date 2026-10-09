#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using DG.DOTweenEditor;
using System.Reflection;

[CustomEditor(typeof(UIViewAnimator))]
public class UIViewAnimatorEditor : Editor
{
    SerializedProperty showAnimProp;
    SerializedProperty hideAnimProp;

    int showTab;
    int hideTab;
    bool showFoldout = true;
    bool hideFoldout = true;
    GUIContent iconOn;
    GUIContent iconOff;

    readonly string[] tabs = { "Move", "Scale", "Fade", "Rotate" };

    struct Snapshot
    {
        public Vector3 pos;
        public Vector3 scale;
        public Vector3 angle;
        public float alpha;
        public UIViewAnimator.UIAnimState state;
    }

    UIViewAnimator anim;
    RectTransform rect;
    CanvasGroup canvas;

    Tween moveTween;
    Tween scaleTween;
    Tween fadeTween;
    Tween rotateTween;

    Snapshot snapshot;
    bool hasSnapshot;

    FieldInfo moveTweenField;
    FieldInfo scaleTweenField;
    FieldInfo fadeTweenField;
    FieldInfo rotateTweenField;
    PropertyInfo stateProp;

    void OnEnable()
    {
        iconOn = EditorGUIUtility.IconContent("TestPassed");
        iconOff = EditorGUIUtility.IconContent("TestIgnored");

        showAnimProp = serializedObject.FindProperty("showAnim");
        hideAnimProp = serializedObject.FindProperty("hideAnim");

        anim = (UIViewAnimator)target;
        rect = anim.GetComponent<RectTransform>();
        canvas = anim.GetComponent<CanvasGroup>();

        moveTweenField = typeof(UIViewAnimator)
            .GetField("moveTween", BindingFlags.Instance | BindingFlags.NonPublic);

        fadeTweenField = typeof(UIViewAnimator)
            .GetField("fadeTween", BindingFlags.Instance | BindingFlags.NonPublic);

        scaleTweenField = typeof(UIViewAnimator)
            .GetField("scaleTween", BindingFlags.Instance | BindingFlags.NonPublic);

        rotateTweenField = typeof(UIViewAnimator)
            .GetField("rotateTween", BindingFlags.Instance | BindingFlags.NonPublic);

        stateProp = typeof(UIViewAnimator)
            .GetProperty("State", BindingFlags.Instance | BindingFlags.Public);
    }

    void OnDisable()
    {
        StopPreview();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultScriptFields();

        GUILayout.Space(8);

        DrawAnimFoldout("Show Animation", showAnimProp, ref showFoldout, ref showTab);

        GUILayout.Space(6);

        DrawAnimFoldout( "Hide Animation", hideAnimProp, ref hideFoldout, ref hideTab);

        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(12);
        EditorGUILayout.LabelField("Editor Preview", EditorStyles.boldLabel);

        if (Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Editor Preview disabled in Play Mode", MessageType.Info);
            return;
        }

        if (GUILayout.Button("Preview Show"))
        {
            StartPreview();
            anim.EditorPreviewShow();
            HookPreviewSeq();
        }

        if (GUILayout.Button("Preview Hide"))
        {
            StartPreview();
            anim.EditorPreviewHide();
            HookPreviewSeq();
        }

        if (GUILayout.Button("Stop & Reset"))
        {
            StopPreview();
        }
    }

    // ================= CORE =================

    void StartPreview()
    {
        StopPreview();
        TakeSnapshot();
        DOTweenEditorPreview.Start();
    }

    void StopPreview()
    {
        anim.KillTweens();
        
        
        moveTween?.Kill(true);
        moveTween = null;

        fadeTween?.Kill(true);
        fadeTween = null;

        scaleTween?.Kill(true);
        scaleTween = null;

        rotateTween?.Kill(true);
        rotateTween = null;

        DOTweenEditorPreview.Stop();
        DOTween.Clear(true);
        RestoreSnapshot();
    }

    void HookPreviewSeq()
    {
        moveTween = moveTweenField?.GetValue(anim) as Tween;
        fadeTween = fadeTweenField?.GetValue(anim) as Tween;
        scaleTween = scaleTweenField?.GetValue(anim) as Tween;
        rotateTween = rotateTweenField?.GetValue(anim) as Tween;

        if (moveTween != null)
            DOTweenEditorPreview.PrepareTweenForPreview(moveTween);
        if (fadeTween != null)
            DOTweenEditorPreview.PrepareTweenForPreview(fadeTween);
        if (scaleTween != null)
            DOTweenEditorPreview.PrepareTweenForPreview(scaleTween);
        if (rotateTween != null)
            DOTweenEditorPreview.PrepareTweenForPreview(rotateTween);
    }

    // ================= SNAPSHOT =================

    void TakeSnapshot()
    {
        snapshot = new Snapshot
        {
            pos = rect.anchoredPosition3D,
            scale = rect.localScale,
            alpha = canvas.alpha,
            state = anim.State,
            angle = rect.localEulerAngles
        };
        hasSnapshot = true;
    }

    void RestoreSnapshot()
    {
        if (!hasSnapshot) return;

        if (rect != null)
        {
            rect.anchoredPosition3D = snapshot.pos;
            rect.localScale = snapshot.scale;
            rect.localEulerAngles = snapshot.angle;
        }

        if (canvas != null)
        {
            canvas.alpha = snapshot.alpha;
        }

        stateProp?.SetValue(anim, snapshot.state);

        hasSnapshot = false;
    }

    // ================= FOLDOUT =================

    void DrawAnimFoldout(string title,SerializedProperty animProp, ref bool foldout, ref int tab)
    {
        foldout = EditorGUILayout.Foldout( foldout, title, true, EditorStyles.foldoutHeader);

        if (!foldout)
            return;

        EditorGUI.indentLevel++;

        tab = DrawTabsWithIcon(animProp, tab);
        GUILayout.Space(4);

        bool isFade = tabs[tab] == "Fade";
        SerializedProperty setting = GetSettingByTab(animProp, tab);

        DrawAnimSetting(setting, isFade);

        EditorGUI.indentLevel--;
    }

    // ================= DRAW SETTING =================

    void DrawAnimSetting(SerializedProperty s, bool isFade)
    {
        var enabledProp = s.FindPropertyRelative("enabled");
        EditorGUILayout.PropertyField(enabledProp);

        if (!enabledProp.boolValue)
            return;
        GUILayout.Space(4);
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.PropertyField(s.FindPropertyRelative("durationAnim"));
        EditorGUILayout.PropertyField(s.FindPropertyRelative("ease"));
        EditorGUILayout.PropertyField(s.FindPropertyRelative("delay"));
        EditorGUILayout.EndVertical();
        GUILayout.Space(4);

        EditorGUILayout.BeginVertical("box");
        if (isFade)
        {
            DrawFadeField("Start Alpha", s.FindPropertyRelative("customStart"));
            DrawFadeField("End Alpha", s.FindPropertyRelative("customEnd"));
        }
        else
        {
            EditorGUILayout.PropertyField(s.FindPropertyRelative("customStart"));
            EditorGUILayout.PropertyField(s.FindPropertyRelative("customEnd"));
        }
        EditorGUILayout.EndVertical();

        GUILayout.Space(4);
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.PropertyField(s.FindPropertyRelative("loopCount"));
        EditorGUILayout.PropertyField(s.FindPropertyRelative("loopType"));
        EditorGUILayout.EndVertical();
    }

    void DrawFadeField(string label, SerializedProperty vec3Prop)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(label, GUILayout.Width(90));

        Vector3 v = vec3Prop.vector3Value;
        v.z = EditorGUILayout.Slider(v.z, 0f, 1f);

        vec3Prop.vector3Value = v;
        EditorGUILayout.EndHorizontal();
    }
    // ================= HELPERS =================

    SerializedProperty GetSettingByTab(SerializedProperty anim, int tab)
    {
        switch (tab)
        {
            case 0: return anim.FindPropertyRelative("move");
            case 1: return anim.FindPropertyRelative("scale");
            case 2: return anim.FindPropertyRelative("fade");
            case 3: return anim.FindPropertyRelative("rotate");
        }
        return null;
    }

    void DrawDefaultScriptFields()
    {
        SerializedProperty it = serializedObject.GetIterator();
        bool enterChildren = true;

        while (it.NextVisible(enterChildren))
        {
            enterChildren = false;

            if (it.name == "showAnim" || it.name == "hideAnim")
                continue;

            EditorGUILayout.PropertyField(it, true);
        }
    }

    int DrawTabsWithIcon(SerializedProperty animProp, int current)
    {
        EditorGUILayout.BeginHorizontal();

        for (int i = 0; i < tabs.Length; i++)
        {
            SerializedProperty setting = GetSettingByTab(animProp, i);
            bool enabled = setting.FindPropertyRelative("enabled").boolValue;

            GUIStyle style = new GUIStyle(EditorStyles.toolbarButton)
            {
                fontStyle = enabled ? FontStyle.Bold : FontStyle.Normal,
                alignment = TextAnchor.MiddleCenter
            };

            GUIContent content = new GUIContent(
                $"{tabs[i]}",
                enabled ? iconOn.image : iconOff.image,
                enabled ? "Enabled" : "Disabled"
            );

            bool pressed = GUILayout.Toggle(current == i, content, style);

            if (pressed)
                current = i;
        }

        EditorGUILayout.EndHorizontal();
        return current;
    }
}
#endif
