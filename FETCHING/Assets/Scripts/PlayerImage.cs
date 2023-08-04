using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerImage : MonoBehaviour
{
    [SerializeField] private GameObject playerImage;

    public void OnImageLoadingButtonClicked()
    {
        //string filePath = dialog.FileName;
        //byte[] fileData = System.IO.File.ReadAllBytes(filePath);
        //Texture2D texture = new Texture2D(2, 2);
        //texture.LoadImage(fileData);
        //Sprite createdSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        Image img = playerImage.GetComponent<Image>();
        img.color = new Color(1, 1, 1, 1);
        //img.sprite = createdSprite;
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
}
