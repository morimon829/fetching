using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndScript : MonoBehaviour
{
    //  時間切れ表示
    private GameObject turnEndObject;
    //AnswerClickActionの情報
    private AnswerClickAction answerClickAction; 
    //timeCounterscriptの情報
    private TimeCounter timeCounterscript;
    //決定ボタン
    private GameObject decisionButton;
    //キャンセルボタン
    private GameObject cancelButton;

    void Start()
    {
        // 時間切れ表示オブジェクトの情報を取得
        turnEndObject = GameObject.Find("TurnEnd");
        decisionButton = GameObject.Find("DecisionButton");
        cancelButton = GameObject.Find("CancelButton");
    }

    public void oNDecisionButton()
    {
        // カウントダウンテキストスクリプトの情報を取得
        timeCounterscript = GameObject.Find("CountdowText").GetComponent<TimeCounter>();
        // カウントダウン時間を0秒に
        timeCounterscript.countDownEndflg = true;
        // 終了処理を実行
        turnEnd();
    }

    public void turnEnd()
    {
        // 回答クリックアクションスクリプトを取得
        answerClickAction = GameObject.Find("Canvas").GetComponent<AnswerClickAction>();

        //ボタンを非表示
        decisionButton.SetActive( false );
        cancelButton.SetActive( false );
        // 時間切れ表示オブジェクトの表示
        turnEndObject.gameObject.SetActive( true );
        // サークル記入フラグをオンにする
        answerClickAction.circleEntryFlag = true;

    //     // // イベントに登録
    //     // SceneManager.sceneLoaded += GameSceneLoaded;

    //     // // シーン切り替え
    //     // SceneManager.LoadScene("GameScene");
    }

    //移行先のシーンを先に作成する
    // private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    // {
    //     // // シーン切り替え後のスクリプトを取得
    //     // var gameManager= GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
        
    //     // // データを渡す処理
    //     // gameManager.score = 100;

    //     // // イベントから削除
    //     // SceneManager.sceneLoaded -= GameSceneLoaded;    
    // }
}
