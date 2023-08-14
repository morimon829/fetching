using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SFB;

public class PlayerImage : MonoBehaviour
{
    [SerializeField] private GameObject playerImage;

    public void OnImageLoadingButtonClicked()
    {
        var extensions = new[] 
        {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg"),
        };

        var paths = StandaloneFileBrowser.OpenFilePanel("画像ファイルを選択してください", "", extensions, false);
        if (paths.Length > 0) {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }

    public void OnCancelButtonClicked()
    {
        Image img = playerImage.GetComponent<Image>();
        img.color = new Color(1, 1, 1, 0);
        img.sprite = null;
    }

    public void OnOkButtonClicked()
    {
        SceneManager.sceneLoaded += NextSceneLoaded;

        SceneManager.LoadScene("DealerAnswer");
    }

    private void NextSceneLoaded(Scene next, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= NextSceneLoaded;
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
            Sprite createdSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            Image img = playerImage.GetComponent<Image>();
            img.color = new Color(1, 1, 1, 1);
            img.sprite = createdSprite;
        }
    }
}
