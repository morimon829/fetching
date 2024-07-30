using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GotoLobby : MonoBehaviour
{
    //ルームIDボックスゲームオブジェクト
    [SerializeField]
    private TMP_InputField _roomIdTextBox;
    //ルームIDボックス注意メッセージ
    [SerializeField]
    private GameObject _roomIdAlert;

    void Start()
    {
        //名前入力注意メッセージを非表示
        _roomIdAlert.gameObject.SetActive(false);
    }

    //ルームを作るボタン押下処理
    public void GotoHostRoom()
    {
        // イベントに登録
        SceneManager.sceneLoaded += StarteMenuSceneLoaded;
        // シーン切り替え
        // SceneManager.LoadScene("Lobby");
        SceneManager.LoadScene("ImageLoad");
    }

    //ルームに入るボタン押下処理
    public void GotoGuestRoom()
    {
        //入力チェック
        if (string.IsNullOrEmpty(_roomIdTextBox.text))
        {
            _roomIdAlert.gameObject.SetActive(true);
        }
        else
        {
            // イベントに登録
            SceneManager.sceneLoaded += StarteMenuSceneLoaded;
            // シーン切り替え
            SceneManager.LoadScene("Lobby");
        }
    }
    private void StarteMenuSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // // シーン切り替え後のスクリプトを取得
        // StarteMenuPrepare starteMenuPrepare = GameObject.FindWithTag("GameManager").GetComponent<StarteMenuPrepare>();
        ImageLoadPrepare imageLoadPrepare = GameObject.FindWithTag("GameManager").GetComponent<ImageLoadPrepare>();

        // // データを渡す処理
        // starteMenuPrepare.Playername = _roomIdTextBox.text;

        // イベントから削除
        SceneManager.sceneLoaded -= StarteMenuSceneLoaded;
    }

}
