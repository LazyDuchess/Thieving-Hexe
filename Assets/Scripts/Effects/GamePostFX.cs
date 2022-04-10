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

    private void Awake()
    {
        charOverlayRT = new RenderTexture(Screen.width, Screen.height, 32);
        lastWidth = Screen.width;
        lastHeight = Screen.height;
        XRayCamera.targetTexture = charOverlayRT;
        XRayCamera.forceIntoRenderTexture = true;
        postFXMaterial.SetTexture("_Overlay", charOverlayRT);
    }

    private void Update()
    {
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            Destroy(charOverlayRT);
            charOverlayRT = new RenderTexture(Screen.width, Screen.height, 32);
            lastWidth = Screen.width;
            lastHeight = Screen.height;
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
