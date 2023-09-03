using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SFB;

public class ImageLoadScript : MonoBehaviour
{
    // 読込画像用のオブジェクト
    [SerializeField]
    private Image _image;
    // 次に呼ばれるシーン名
    private static readonly string NextScene = "DealerAnswer";
    // シーン間引継ぎ情報
    private ImageLoadPrepare _imageLoadPrepare;

    public void OnImageLoadButtonClicked()
    {
        // ファイル形式の指定
        var extensions = new[] 
        {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg"),
        };

        // 画像ファイルのパスを取得
        var paths = StandaloneFileBrowser.OpenFilePanel("画像ファイルを選択してください", "", extensions, false);

        if (paths.Length > 0) {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
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

        dealerAnswerPrepare.CreatedSprite = _image.sprite;
        dealerAnswerPrepare.Player1Score = _imageLoadPrepare.Player1Score;
        dealerAnswerPrepare.Player2Score = _imageLoadPrepare.Player2Score;
        dealerAnswerPrepare.Player1Name = _imageLoadPrepare.Player1Name;
        dealerAnswerPrepare.Player2Name = _imageLoadPrepare.Player2Name;

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    private IEnumerator OutputRoutine(string url)
    {
        var request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (UnityWebRequest.Result.Success != request.result)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            // 取得した画像のテクスチャをスプライトに変換
            Sprite createdSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            _image.color = new Color(1, 1, 1, 1);
            _image.sprite = createdSprite;
        }
    }
}
