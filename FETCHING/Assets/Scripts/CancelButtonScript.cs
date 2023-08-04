using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CancelButtonScript : MonoBehaviour
{
    //AnswerClickActionを宣言
    private AnswerClickAction answerClickAction; 
    //決定ボタン
    private GameObject decisionButton;
    //キャンセルボタン
    private GameObject cancelButton;
    // 円予測表示
    private GameObject circlePrediction;

    void Start()
    {
        //各種オブジェクトを取得
        answerClickAction = GameObject.Find("Canvas").GetComponent<AnswerClickAction>();
        decisionButton = GameObject.Find("DecisionButton");
        cancelButton = GameObject.Find("CancelButton");
        circlePrediction = GameObject.Find("CirclePrediction");
    }

    public void ClickButton()
    {
        //キャンセル処理コルーチンを呼び出す
        StartCoroutine("CancelAction");
    }

    private IEnumerator CancelAction() 
    {
        //0.2秒後にキャンセル処理を実施
        //waitを挟まないとボタン押下時にサークルが描画される為
        yield return new WaitForSeconds(0.2f);

        // プレイヤーが作成したサークルを削除
        Destroy(answerClickAction.choiceCircle);
        // 円予測非表示
        circlePrediction.SetActive( false );
        //ボタンを非表示
        decisionButton.SetActive( false );
        cancelButton.SetActive( false );

        //サークル記入フラグをオフにする
        answerClickAction.circleEntryFlag = false;
    } 

}
