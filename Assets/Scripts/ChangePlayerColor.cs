using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangePlayerColor : MonoBehaviour
{
    public Color[] originalColors;
    public Color[] newColors;
    public Key swapKey;
    public Texture2D texture2D;
    public List<List<Point2>> pointToSwap;

    private Texture2D textureTemp;

    private void OnValidate() {
        pointToSwap = new List<List<Point2>>();
        if(pointToSwap.Count != originalColors.Length)
            return;
        
        for (int i = 0; i < 5; i++)
        {
            var templist = new List<Point2>();
            pointToSwap.Add(templist);
        }
    }

    private void Start() {
        texture2D = GetComponent<SpriteRenderer>().sprite.texture;
        textureTemp = new Texture2D(texture2D.width, texture2D.height);
        textureTemp.filterMode = FilterMode.Point;
        textureTemp.wrapMode = TextureWrapMode.Clamp;
        for (int i = 0; i < 5; i++)
        {
            var templist = new List<Point2>();
            pointToSwap.Add(templist);
        }
        GetColorPosition(originalColors);
    }

    private void GetColorPosition(Color[] targetColor){
        for (int i = 0; i < texture2D.width; i++)
        {
            for (int j = 0; j < texture2D.height; j++)
            {
                for (int k = 0; k < targetColor.Length; k++)
                {
                    if(targetColor[k] == texture2D.GetPixel(i, j))
                        pointToSwap[k].Add(new Point2(i, j));
                }
            }
        }
    }

    private void SetColor(){
        for (int i = 0; i < pointToSwap.Count; i++)
        {
            foreach (var point in pointToSwap[i])
            {
                textureTemp.SetPixel(point.x, point.y, newColors[i]);
            }
        }
        textureTemp.Apply();
        texture2D = textureTemp;
    }

    public void ChangeColorByIndex(Color color, int index){
        newColors[index] = color;
    }

    public void CommintSetColor(){
        SetColor();
    }
}
