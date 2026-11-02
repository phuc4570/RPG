using UnityEngine;
using TMPro;
using DG.Tweening;

public class StatueCheckpointUIAnim : MonoBehaviour
{
    [SerializeField] private SpriteRenderer statueSprite;
    [SerializeField] private TextMeshPro interactText;

    [SerializeField] private float duration = 0.3f;
    [SerializeField] private float startLocalY = 2.5f;
    [SerializeField] private float targetLocalY = 3.0f;

    private Sequence currentSequence;

    private void Awake()
    {
        SetVisualAlpha(0f);
        SetLocalY(startLocalY);
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        currentSequence?.Kill();

        currentSequence = DOTween.Sequence();

        if (statueSprite != null)
            currentSequence.Join(statueSprite.DOFade(1f, duration));

        if (interactText != null)
            currentSequence.Join(interactText.DOFade(1f, duration));

        currentSequence.Join(transform.DOLocalMoveY(targetLocalY, duration).SetEase(Ease.OutBack));

        currentSequence.Play();
    }

    public void Hide()
    {
        currentSequence?.Kill();

        currentSequence = DOTween.Sequence();

        if (statueSprite != null)
            currentSequence.Join(statueSprite.DOFade(0f, duration));

        if (interactText != null)
            currentSequence.Join(interactText.DOFade(0f, duration));

        currentSequence.Join(transform.DOLocalMoveY(startLocalY, duration).SetEase(Ease.InQuad));

        currentSequence.OnComplete(() => {
            gameObject.SetActive(false);
        });

        currentSequence.Play();
    }

    private void SetVisualAlpha(float alpha)
    {
        if (statueSprite != null)
        {
            Color c = statueSprite.color;
            c.a = alpha;
            statueSprite.color = c;
        }

        if (interactText != null)
        {
            Color c = interactText.color;
            c.a = alpha;
            interactText.color = c;
        }
    }

    private void SetLocalY(float y)
    {
        Vector3 pos = transform.localPosition;
        pos.y = y;
        transform.localPosition = pos;
    }
}
