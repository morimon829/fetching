using Kakera;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AddImage : MonoBehaviour
{
    [SerializeField] private Unimgpicker imagePicker;
    [SerializeField] private Image ssImage;
    public Texture2D texture;
    public Sprite texture2;

    private void Awake()
    {
        imagePicker.Completed += path => StartCoroutine(LoadImage(path, ssImage));
    }

    private void Start()
    {
        if (!ssImage)
        {
            ssImage = gameObject.GetComponent<Image>();
        }
    }

    public void OnPressShowPicker()
    {
        imagePicker.Show("Select Image", "unimgpicker", 512);//1024��512�ɕύX
    }

    private IEnumerator LoadImage(string path, Image output)
    {
        Debug.Log("LoadImage");
        string url = "file://" + path;
        WWW www = new WWW(url);
        yield return www;


        texture = www.texture;
        int _CompressRate = TextureCompressionRate.TextureCompressionRatio(texture.width, texture.height);
        TextureScale.Bilinear(texture, texture.width / _CompressRate, texture.height / _CompressRate);
        texture2 = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        output.sprite = texture2;
    }
}


public static class TextureCompressionRate
{
    /// <summary>
    /// Texture��500x500�Ɏ��܂�悤�Ƀ��T�C�Y���܂�
    /// </summary>
    public static int TextureCompressionRatio(int width, int height)
    {
        if (width >= height)
        {
            if (width / 500 > 0) return (width / 500);
            else return 1;
        }
        else if (width < height)
        {
            if (height / 500 > 0) return (height / 500);
            else return 1;
        }
        else return 1;
    }
}