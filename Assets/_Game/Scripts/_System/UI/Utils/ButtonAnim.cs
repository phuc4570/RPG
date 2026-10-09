using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Script giúp thực hiện anim scale của Button khi nhấn vào
[RequireComponent(typeof(Button))]
public class ButtonAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Button
    [SerializeField, ReadOnly] Button button;

    //Animation scale sẽ được thực hiện trên Transform này
    [SerializeField, ReadOnly] Transform ts;

    //Giá trị scale
    [SerializeField, LabelText("@GetLabel()")] Vector3 value = Vector3.one * 0.1f;

    //Kiểu scale, nếu relative = true => tính value dựa trên start value của Transform, ngược lại relative = false => scale tới giá trị của value
    [SerializeField] bool relative = true;

    //Thời gian thực hiện animation scale
    [SerializeField] float duration = 0.1f;

    //Kiểu ease để thực hiện animation scale
    [SerializeField] Ease Ease = Ease.Linear;

    //Ghi lại giá trị scale ban đầu
    private Vector3 startValue;

    //Ghi lại giá trị scale cuối cùng
    private Vector3 endValue;

    //Biến kiểm soát tránh việc thực hiện animation mới khi animation cũ chưa thực hiện xong
    private bool canDoNewAnimation = true;

    //Thực hiện việc lấy thông tin Transform
    private void OnValidate()
    {
        if(!ts) ts = GetComponent<Transform>();
        if (!button) button = GetComponent<Button>();
    }

    //Dựa vào biến relative sẽ thay đổi label của biến value
    private string GetLabel()
    {
        return relative ? "Factor" : "End value";
    }

    //Ghi lại giá trị ban đầu và tính toán giá trị cuối cùng
    private void Awake()
    {
        OnValidate();
        startValue = ts.localScale;
        endValue = relative ? startValue + value : value;
    }

    //Hàm xây dựng từ interface IPointerDownHandler
    public void OnPointerDown(PointerEventData eventData)
    {
        //Kiểm tra xem button có đang enable hoặc interactable hay không mới diễn hoạt animation
        if (button.enabled && button.interactable && canDoNewAnimation)
        {
            canDoNewAnimation = false;
            ts?.DOScale(endValue, duration).SetEase(Ease);
        }
    }

    //Hàm xây dựng từ interface IPointerUpHandler
    public void OnPointerUp(PointerEventData eventData)
    {
        ts?.DOScale(startValue, duration).SetEase(Ease).OnComplete(() =>
        {
            canDoNewAnimation = true;
        });
    }
}
