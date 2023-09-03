using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TurnEndScript : MonoBehaviour
{
    // turnEndオブジェクト
    [SerializeField]
    private GameObject _turnEndObject;
    // 決定ボタンオブジェクト
    [SerializeField]
    private GameObject _decisionButton;
    // キャンセルボタンオブジェクト
    [SerializeField]
    private GameObject _cancelButtonObject;
    // AnswerClickActionの情報
    [SerializeField]
    private AnswerClickAction _answerClickAction;
    // timeCounterscriptの情報
    [SerializeField]
    private TimeCounter _timeCounterscript;
    //親と子の処理判定
    [SerializeField]
    private string _processedSCENE;

    public void OnDecisionButton()
    {
        // カウントダウン時間を0秒に
        _timeCounterscript.countDownEndflg = true;
        // 終了処理を実行
        TurnEnd();
    }

    public void TurnEnd()
    {
        // ボタンを非表示
        _decisionButton.SetActive(false);
        _cancelButtonObject.SetActive(false);
        // 時間切れ表示オブジェクトの表示
        _turnEndObject.gameObject.SetActive(true);
        // サークル記入フラグをオンにする
        _answerClickAction.circleEntryFlag = true;
        //稼働しているシーンで分岐
        if (_processedSCENE == "DealerAnswer")
        {
            // イベントに登録
            SceneManager.sceneLoaded += ChildAnswerGameSceneLoaded;
            // シーン切り替え
            SceneManager.LoadScene("ChildAnswer");
        }
        else if (_processedSCENE == "ChildAnswer")
        {
            // イベントに登録
            SceneManager.sceneLoaded += GameResultGameSceneLoaded;
            // シーン切り替え
            SceneManager.LoadScene("GameResult");
        }
    }

    private void ChildAnswerGameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        AnswerPrepare answerPrepare = GameObject.FindWithTag("GameManager").GetComponent<AnswerPrepare>();

        // データを渡す処理
        answerPrepare.startPos = _answerClickAction.startPos;
        answerPrepare.endPos = _answerClickAction.endPos;

        // イベントから削除
        SceneManager.sceneLoaded -= ChildAnswerGameSceneLoaded;
    }

    private void GameResultGameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        GameResultPrepare gameResultPrepare = GameObject.FindWithTag("GameManager").GetComponent<GameResultPrepare>();

        // データを渡す処理
        gameResultPrepare.startPos = _answerClickAction.startPos;
        gameResultPrepare.endPos = _answerClickAction.endPos;

        // イベントから削除
        SceneManager.sceneLoaded -= GameResultGameSceneLoaded;
    }
}
