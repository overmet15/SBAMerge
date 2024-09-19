using System.Collections;
using UnityEditor;
using UnityEngine;

public class ScreenshotTaker : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private string imageName;
    [SerializeField] private int shotWidth, shotHeight;
    void Start()
    {
        _camera = GetComponent<Camera>();
        StartCoroutine(TakeScreenshot(Application.dataPath + $"/{imageName}.png", shotWidth, shotHeight));
    }

    IEnumerator TakeScreenshot(string path, int width, int height)
    {
        yield return new WaitForEndOfFrame();
        RenderTexture rt = new(width, height, 24);

        _camera.targetTexture = rt;
        Texture2D screenShot = new(width, height, TextureFormat.RGBA32, false);
        _camera.Render();
        screenShot.ReadPixels(new(0, 0, width, height), 0, 0);
        _camera.targetTexture = null;
        RenderTexture.active = null;
        if (Application.isEditor) DestroyImmediate(rt);
        else Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, bytes);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
