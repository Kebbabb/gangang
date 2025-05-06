using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class Phone : MonoBehaviour
{
    public RawImage screen;
    public Texture2D[] sourceImages; // Assign in Inspector

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

        combinedTexture.Apply();
        targetMaterial.mainTexture = combinedTexture;
        combinedTexture.wrapMode = TextureWrapMode.Clamp;
        screen.material = targetMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
