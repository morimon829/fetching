using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRound : MonoBehaviour
{
    public void OnBottan()
    {
        // イベントに登録
        SceneManager.sceneLoaded += ImageLoadGameSceneLoaded;
        // シーン切り替え
        SceneManager.LoadScene("ImageLoad");

    }

    private void ImageLoadGameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // // シーン切り替え後のスクリプトを取得
        // ChildAnswerPrepare childAnswerPrepare = GameObject.FindWithTag("GameManager").GetComponent<ChildAnswerPrepare>();

        // // データを渡す処理
        // childAnswerPrepare.startPos = _answerClickAction.startPos;
        // childAnswerPrepare.endPos = _answerClickAction.endPos;
        // childAnswerPrepare.pushendPos = _answerClickAction.pushendPos;

        // イベントから削除
        SceneManager.sceneLoaded -= ImageLoadGameSceneLoaded;
    }
}
