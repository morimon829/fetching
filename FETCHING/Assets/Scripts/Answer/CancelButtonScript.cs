using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CancelButtonScript : MonoBehaviour
{
    // 決定ボタンオブジェクト
    [SerializeField]
    private GameObject _decisionButtonObject;
    // キャンセルボタンオブジェクト
    [SerializeField]
    private GameObject _cancelButtonObject;
    // 円予測表示オブジェクト
    [SerializeField]
    private GameObject _circlePredictionObject;
    // AnswerClickActionの情報
    [SerializeField]
    private AnswerClickAction _answerClickAction;

    public void ClickButton()
    {
        // キャンセル処理コルーチンを呼び出す
        StartCoroutine("CancelAction");
    }

    private IEnumerator CancelAction()
    {
        // 0.2f後にキャンセル処理を実施
        // waitを挟まないとボタン押下時にサークルが描画される為
        yield return new WaitForSeconds(0.2f);

        // プレイヤーが作成したサークルを削除
        Destroy(_answerClickAction.choiceCircleObject);
        // 円予測非表示
        _circlePredictionObject.SetActive(false);
        // ボタンを非表示
        _decisionButtonObject.SetActive(false);
        _cancelButtonObject.SetActive(false);

        // サークル記入フラグをオフにする
        _answerClickAction.circleEntryFlag = false;
    }

}
