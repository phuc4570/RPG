using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private SoundTypeEnum soundType = SoundTypeEnum.Click;
    private Button button;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(button == null)
        {
            button = GetComponent<Button>();
        }
    }
#endif
    private void Awake()
    {
        OnValidate();
    }

    private void OnEnable()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClicked);
            button.onClick.AddListener(OnButtonClicked);
        }
    }

    private void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }
    }

    private void OnButtonClicked()
    {
        if (SoundManager.Ins != null)
        {
            SoundManager.Ins.PlaySound(soundType);
        }
    }
}