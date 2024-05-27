using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SFB;
using Kakera;

public class ImageLoad : MonoBehaviour
{
    // 読込画像用のオブジェクト
    [SerializeField]
    private Image _image;
    // 次に呼ばれるシーン名
    private static readonly string NextScene = "DealerAnswer";
    // シーン間引継ぎ情報
    private ImageLoadPrepare _imageLoadPrepare;
    private Sprite _sprite;
    //Unimgpickerプレハブ
    [SerializeField]
    private Unimgpicker imagePicker;
    //Texture2D情報
    public Texture2D texture;

    private void Awake()
    {
        imagePicker.Completed += path => StartCoroutine(LoadImage(path, _image));
    }

    public void OnPressShowPicker()
    {
        imagePicker.Show("Select Image", "unimgpicker", 1024);//1024��512�ɕύX
    }

    public void OnCancelButtonClicked()
    {
        // 読込画像を削除
        _image.color = new Color(1, 1, 1, 0);
        _image.sprite = null;
    }

    public void OnOkButtonClicked()
    {
        _imageLoadPrepare = GameObject.FindWithTag("GameManager").GetComponent<ImageLoadPrepare>();
        SceneManager.sceneLoaded += GameSceneLoaded;

        SceneManager.LoadScene(NextScene);
    }

    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        DealerAnswerPrepare dealerAnswerPrepare = GameObject.FindWithTag("GameManager").GetComponent<DealerAnswerPrepare>();

        dealerAnswerPrepare.CreatedSprite = _sprite;
        dealerAnswerPrepare.ScoreList = _imageLoadPrepare.ScoreList;
        dealerAnswerPrepare.Player1Name = _imageLoadPrepare.Player1Name;
        dealerAnswerPrepare.Player2Name = _imageLoadPrepare.Player2Name;

        SceneManager.sceneLoaded -= GameSceneLoaded;
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
        // _sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        _sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        output.sprite = _sprite;
        output.color = new Color(1, 1, 1, 1);
        output.preserveAspect = true;
    }
}

