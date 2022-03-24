using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GamePostFX : MonoBehaviour
{
    public Material postFXMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Copy the source Render Texture to the destination,
        // applying the material along the way.
        Graphics.Blit(src, dest, postFXMaterial);
    }
}
