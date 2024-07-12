using UnityEngine;

public class CameraAspectAdjust : MonoBehaviour
{
    private float targetHeight;

    [SerializeField] private float mobileTarget;
    [SerializeField] private float standaloneTarget;
    public bool isEditor;
#if UNITY_EDITOR
    void Start()
    {
        isEditor = true;
    }
#endif
    void Update()
    {
#if UNITY_STANDALONE
        targetHeight = standaloneTarget;
#elif UNITY_ANDROID
        if (!isEditor) targetHeight = mobileTarget;
        else targetHeight = standaloneTarget;
#endif
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float aspectRatio = 0;
        if (screenHeight > screenWidth) aspectRatio = screenWidth / screenHeight;
        else aspectRatio = screenHeight / screenWidth;

        float s = targetHeight / aspectRatio;
        Camera.main.orthographicSize = s / 3f;
    }
}
