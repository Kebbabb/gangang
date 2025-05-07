using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class Phone : MonoBehaviour
{
    public RawImage screen;
    public Texture2D[] sourceImages; // Assign in Inspector
    private float textureTiling = 1f;
    public AnimationCurve scrollCurve;
    public float scrollTime = 2; // Speed of the scroll
    private bool isScrolling = false;
    public int pictureWidth = 1080;
    public int picturePadding = 0;

    public int scrollIndex = 0;
    public Color backgroundColor = new Color(1, 1, 1, 1); // Transparent background color
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetImagesToTexture();
    }
    void SetImagesToTexture(){
        // create a new material with the shader of your choice
        Material targetMaterial = new Material(Shader.Find("Unlit/Texture"));
        if (sourceImages.Length == 0) return;

        int width = pictureWidth;
        int height = 0;

        int heightI = (int)screen.rectTransform.sizeDelta.y * pictureWidth;

        foreach (var tex in sourceImages)
            height += heightI;

        Color[] fillColorArray = new Color[width * height];

        // Fill with background color
        for (int i = 0; i < fillColorArray.Length; i++)
            fillColorArray[i] = backgroundColor;

        Texture2D combinedTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        combinedTexture.SetPixels(fillColorArray); // Set all pixels
        int offsetY = 0;
        for (int i = sourceImages.Length - 1; i >= 0; i--) // Draw bottom to top
        {
            Texture2D src = ResizeTextureToWidth(sourceImages[i], pictureWidth - picturePadding * 2);
            int startHeight = (heightI - src.height) / 2;
            combinedTexture.SetPixels(picturePadding, offsetY + startHeight, src.width, src.height, src.GetPixels());
            offsetY += heightI;
        }

        textureTiling = 1f / sourceImages.Length;

        combinedTexture.Apply();
        targetMaterial.mainTexture = combinedTexture;
        targetMaterial.mainTextureScale = new Vector2(1, textureTiling);
        targetMaterial.mainTextureOffset = new Vector2(0, 1 - textureTiling);
        combinedTexture.wrapMode = TextureWrapMode.Repeat;
        screen.material = targetMaterial;
    }
    Texture2D ResizeTextureToWidth(Texture2D source, int targetWidth)
    {
        int originalWidth = source.width;
        int originalHeight = source.height;
        float aspect = (float)originalHeight / originalWidth;
        int targetHeight = Mathf.RoundToInt(targetWidth * aspect);

        // Create a new empty texture
        Texture2D result = new Texture2D(targetWidth, targetHeight, TextureFormat.RGBA32, false);

        // Loop through pixels in the new texture and assign based on bilinear interpolation
        for (int y = 0; y < targetHeight; y++)
        {
            for (int x = 0; x < targetWidth; x++)
            {
                float u = x / (float)targetWidth;
                float v = y / (float)targetHeight;
                Color color = source.GetPixelBilinear(u, v);
                result.SetPixel(x, y, color);
            }
        }

        result.Apply();
        return result;
    }

    public void testScroll()
    {
        if (isScrolling) return;
        StartCoroutine(ScrollCoroutine(() => {Debug.Log("Scrolling complete");}));
    }
    public void Scroll(System.Action onComplete = null)
    {
        if(isScrolling) return;
        StartCoroutine(ScrollCoroutine(onComplete));
    }
    private IEnumerator ScrollCoroutine(System.Action onComplete)
    {
        isScrolling = true;
        Debug.Log("Scrolling started");
        Vector2 startOffset = screen.material.mainTextureOffset;
        Vector2 targetOffset = startOffset - new Vector2(0, textureTiling);

        float elapsedTime = 0f;

        while (elapsedTime < scrollTime)
        {
            float t = elapsedTime / scrollTime;
            float curvedT = scrollCurve.Evaluate(t);
            screen.material.mainTextureOffset = Vector2.Lerp(startOffset, targetOffset, curvedT);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scrollIndex++;
        onComplete?.Invoke();
        // Ensure it ends exactly at the target
        screen.material.mainTextureOffset = targetOffset;
        isScrolling = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
