using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class FindUICamera : MonoBehaviour
{
    [SerializeField, ReadOnly] private Canvas canvas;
    [SerializeField, ReadOnly] private Camera uiCamera;

#if UNITY_EDITOR

    //Xử lý việc lấy Canvas và UI Camera khi component được khởi tạo
    private void OnValidate()
    {
        if (!canvas) canvas = GetComponent<Canvas>();
        if (!uiCamera) FindCamera();
    }

#endif

    //Đăng ký sự kiện mỗi khi có scene mới được load sẽ tìm lại UI Camera
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Hàm thực hiện khi có scene được load
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateUICamera();
    }

    //Hàm cập nhật UI Camera
    private void UpdateUICamera()
    {
        FindCamera();

        //Nếu tìm dược UI Camera thì chuyển Canvas sang chế độ render theo UI Camera, ngược lại sẽ theo screen
        if (uiCamera)
        {
            canvas.worldCamera = uiCamera;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
        }
        else
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }

    //Tìm kiếm UI Camera
    private void FindCamera()
    {
        //Tìm UI Camera trong danh sách stack camera overlay của Main Camera
        var stackCamera = Camera.main.GetUniversalAdditionalCameraData();
        for(int i = 0; i < stackCamera.cameraStack.Count; i++)
        {
            if (stackCamera.cameraStack[i].name.Equals("UI Camera"))
            {
                uiCamera = stackCamera.cameraStack[i];
                return;
            }
        }

        //Nếu đã tìm được thì trả về không chạy nữa
        if (uiCamera) return;

        //Nếu chưa tìm được sẽ quét tất cả để tìm object có tên UI Camera
        uiCamera = GameObject.Find("UI Camera")?.GetComponent<Camera>();
    }
}
