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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // create a new material with the shader of your choice
        Material targetMaterial = new Material(Shader.Find("Unlit/Texture"));
        if (sourceImages.Length == 0) return;

        Debug.Log(sourceImages[0].GetPixels());
        int width = sourceImages[0].width;
        int height = 0;

        foreach (var tex in sourceImages)
            height += tex.height;

        Texture2D combinedTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        int offsetY = 0;
        for (int i = sourceImages.Length - 1; i >= 0; i--) // Draw bottom to top
        {
            Texture2D src = sourceImages[i];
            combinedTexture.SetPixels(0, offsetY, src.width, src.height, src.GetPixels());
            offsetY += src.height;
        }

        textureTiling = 1f / sourceImages.Length;

        combinedTexture.Apply();
        targetMaterial.mainTexture = combinedTexture;
        targetMaterial.mainTextureScale = new Vector2(1, textureTiling);
        targetMaterial.mainTextureOffset = new Vector2(0, 1 - textureTiling);
        combinedTexture.wrapMode = TextureWrapMode.Clamp;
        screen.material = targetMaterial;
    }
    public void Scroll()
    {
        StartCoroutine(ScrollCoroutine());
    }
    private IEnumerator ScrollCoroutine()
    {
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

        // Ensure it ends exactly at the target
        screen.material.mainTextureOffset = targetOffset;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
