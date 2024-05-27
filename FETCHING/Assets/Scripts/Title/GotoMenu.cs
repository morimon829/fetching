using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GotoMenu : MonoBehaviour
{
    //名前テキストボックスゲームオブジェクト
    [SerializeField]
    private TMP_InputField _nameTextBox;
    //名前入力注意メッセージ
    [SerializeField]
    private GameObject _nameAlert;

    void Start()
    {
        //名前入力注意メッセージを非表示
        _nameAlert.gameObject.SetActive(false);
    }

    //ゲーム開始ボタン押下処理
    public void GotoStartMenu()
    {
        //入力チェック
        if (string.IsNullOrEmpty(_nameTextBox.text))
        {
            _nameAlert.gameObject.SetActive(true);
        }
        else
        {
            // イベントに登録
            SceneManager.sceneLoaded += StarteMenuSceneLoaded;
            // シーン切り替え
            SceneManager.LoadScene("StarteMenu");
        }
    }
    private void StarteMenuSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        StarteMenuPrepare starteMenuPrepare = GameObject.FindWithTag("GameManager").GetComponent<StarteMenuPrepare>();

        // データを渡す処理
        starteMenuPrepare.Playername = _nameTextBox.text;

        // イベントから削除
        SceneManager.sceneLoaded -= StarteMenuSceneLoaded;
    }

}
