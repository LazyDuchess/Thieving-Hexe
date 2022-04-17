using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePostFX : MonoBehaviour
{
    public Material postFXMaterial;
    RenderTexture charOverlayRT;
    public Camera XRayCamera;
    public int lastWidth;
    public int lastHeight;
    public static GamePostFX instance;
    public bool splitScreen = false;
    public bool unique = true;
    private void Awake()
    {
        var hei = Screen.height;
        if (splitScreen)
            hei /= 2;
        charOverlayRT = new RenderTexture(Screen.width, hei, 32);
        lastWidth = hei;
        lastHeight = Screen.height;
        XRayCamera.targetTexture = charOverlayRT;
        XRayCamera.forceIntoRenderTexture = true;
        if (unique)
            postFXMaterial = Instantiate(postFXMaterial);
        postFXMaterial.SetTexture("_Overlay", charOverlayRT);
        instance = this;
    }

    private void Update()
    {
        var hei = Screen.height;
        if (splitScreen)
            hei /= 2;
        if (Screen.width != lastWidth || hei != lastHeight)
        {
            Destroy(charOverlayRT);
            charOverlayRT = new RenderTexture(Screen.width, hei, 32);
            lastWidth = Screen.width;
            lastHeight = hei;
            XRayCamera.targetTexture = charOverlayRT;
            XRayCamera.forceIntoRenderTexture = true;
            postFXMaterial.SetTexture("_Overlay", charOverlayRT);
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Copy the source Render Texture to the destination,
        // applying the material along the way.
        Graphics.Blit(src, dest, postFXMaterial);
    }
}
