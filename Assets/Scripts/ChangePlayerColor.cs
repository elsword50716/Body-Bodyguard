﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangePlayerColor : MonoBehaviour
{
    public Color[] originalColors;
    public Color[] newColors;
    public Key swapKey;
    public SpriteRenderer spriteRenderer;
    public Texture2D texture2D;
    public Texture2D textureTemp;
    public Point2[][] point2s;

    private void Update()
    {
        if (Keyboard.current[swapKey].wasPressedThisFrame)
        {
            RandomSetColors();
            SetColor();
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        texture2D = spriteRenderer.sprite.texture;
        textureTemp = duplicateTexture(texture2D);
        // textureTemp = new Texture2D(texture2D.width, texture2D.height);
        // textureTemp.SetPixels(texture2D.GetPixels());
        // textureTemp.Apply();

        point2s = GetColorPosition(originalColors);
        for (int i = 0; i < point2s.Length; i++)
        {
            Debug.Log(point2s[i].Length);
        }
    }

    private Point2[][] GetColorPosition(Color[] targetColor)
    {
        Point2[][] point2s_temp = new Point2[targetColor.Length][];
        for (int i = 0; i < targetColor.Length; i++)
        {
            List<Point2> temp = new List<Point2>();
            for (int y = 0; y < textureTemp.height; y++)
            {
                for (int x = 0; x < textureTemp.width; x++)
                {
                    var colorInTexture = textureTemp.GetPixel(x, y);
                    //Debug.Log(colorInTexture);
                    if (colorInTexture == targetColor[i])
                    {
                        temp.Add(new Point2(x, y));
                        //Debug.Log(temp.Count);
                    }
                }
            }
            point2s_temp[i] = new Point2[temp.Count];
            point2s_temp[i] = temp.ToArray();
        }

        return point2s_temp;
    }

    private void SetColor()
    {
        for (int i = 0; i < point2s.Length; i++)
        {
            foreach (var point in point2s[i])
            {
                textureTemp.SetPixel(point.x, point.y, newColors[i]);
            }
        }
        textureTemp.Apply();
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", textureTemp);
        spriteRenderer.SetPropertyBlock(block);

    }

    private Texture2D duplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.filterMode = FilterMode.Point;
        readableText.wrapMode = TextureWrapMode.Clamp;
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

    public void ChangeColorByIndex(Color color, int index)
    {
        newColors[index] = color;
    }

    public void RandomSetColors()
    {
        for (int i = 0; i < newColors.Length; i++)
        {
            newColors[i] = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );
        }
    }

    public void CommintSetColor()
    {
        SetColor();
    }
}
