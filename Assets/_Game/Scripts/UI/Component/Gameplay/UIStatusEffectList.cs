using UnityEngine;
using System.Collections.Generic;
using System;

[ExecuteAlways]
public class UIStatusEffectList : MonoBehaviour
{
    [SerializeField] private float spacing = 0.5f;
    [SerializeField] private GameObject iconPrefab;

    private Dictionary<StatusEffectType, GameObject> activeIcons = new Dictionary<StatusEffectType, GameObject>();
    private List<GameObject> iconPool = new List<GameObject>();

    [Header("Test")]
    [SerializeField] private bool enableEditorPreview = false;
    [SerializeField] private List<StatusEffectType> previewTypesToTest = new List<StatusEffectType>();
    private List<StatusEffectType> lastPreviewTypes = new List<StatusEffectType>();

    public void AddStatusEffect(StatusEffectType type)
    {
        if (activeIcons.ContainsKey(type)) return;

        Sprite targetSprite = VisualStatusConfig.Ins.GetData(type)?.Icon;
        if (targetSprite == null)
        {
            return;
        }

        GameObject iconObj = GetOrCreateIconObject();
        var spriteRenderer = iconObj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = targetSprite;
        }

        iconObj.SetActive(true);
        activeIcons.Add(type, iconObj);

        RefreshLayout();
    }

    public void RemoveStatusEffect(StatusEffectType type)
    {
        if (!activeIcons.TryGetValue(type, out GameObject iconObj)) return;

        iconObj.SetActive(false);
        activeIcons.Remove(type);
        iconPool.Add(iconObj);

        RefreshLayout();
    }

    private void RefreshLayout()
    {
        int count = activeIcons.Count;
        if (count == 0) return;

        float totalWidth = (count - 1) * spacing;
        float startX = -totalWidth / 2f;

        int index = 0;
        foreach (var kvp in activeIcons)
        {
            GameObject iconObj = kvp.Value;
            if (iconObj == null) continue;

            float posX = startX + (index * spacing);

            Vector3 localPos = iconObj.transform.localPosition;
            localPos.x = posX;
            localPos.y = 0f;
            localPos.z = 0f;
            iconObj.transform.localPosition = localPos;

            index++;
        }
    }

    private GameObject GetOrCreateIconObject()
    {
        if (iconPool.Count > 0)
        {
            GameObject obj = iconPool[iconPool.Count - 1];
            iconPool.RemoveAt(iconPool.Count - 1);
            if (obj != null) return obj;
        }

        if (iconPrefab == null)
        {
            return new GameObject("Empty");
        }

        GameObject newObj = Instantiate(iconPrefab, transform);
        return newObj;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (!Application.isPlaying)
        {
            if (enableEditorPreview)
            {
                if (!AreListsEqual(previewTypesToTest, lastPreviewTypes))
                {
                    ClearAllForEditor();
                    foreach (var type in previewTypesToTest)
                    {
                        AddStatusEffect(type);
                    }
                    lastPreviewTypes = new List<StatusEffectType>(previewTypesToTest);
                }
            }
            else
            {
                if (activeIcons.Count > 0 || iconPool.Count > 0)
                {
                    ClearAllForEditor();
                    lastPreviewTypes.Clear();
                }
            }
        }
    }

    private void ClearAllForEditor()
    {
        foreach (var kvp in activeIcons)
        {
            if (kvp.Value != null)
            {
                if (Application.isEditor)
                    DestroyImmediate(kvp.Value);
            }
        }
        activeIcons.Clear();

        foreach (var obj in iconPool)
        {
            if (obj != null)
            {
                if (Application.isEditor)
                    DestroyImmediate(obj);
            }
        }
        iconPool.Clear();
    }

    private bool AreListsEqual(List<StatusEffectType> list1, List<StatusEffectType> list2)
    {
        if (list1.Count != list2.Count) return false;
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i]) return false;
        }
        return true;
    }
#endif
}