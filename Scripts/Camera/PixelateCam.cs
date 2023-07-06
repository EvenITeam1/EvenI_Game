using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PixelateCam : MonoBehaviour
{
    [Range (1, 100)] public int pixelate;
    private void OnRenderImage(RenderTexture source, RenderTexture destination){
        source.filterMode = FilterMode.Point;
        RenderTexture resultTexture = RenderTexture.GetTemporary(source.width / pixelate, source.height / pixelate, 0, source.format) ;
        resultTexture.filterMode = FilterMode.Point;
        Graphics.Blit(source, resultTexture);
        Graphics.Blit (resultTexture, destination);
        RenderTexture.ReleaseTemporary(resultTexture) ;
    }
}