using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerClickAction : MonoBehaviour
{
    // 時間切れ表示オブジェクト
    [SerializeField]
    private GameObject _turnEndObject;

    // 決定ボタンオブジェクト
    [SerializeField]
    private GameObject _decisionButtonObject;

    // キャンセルボタンオブジェクト
    [SerializeField]
    private GameObject _cancelButtonObject;

    // サークルプレハブオブジェクト
    public GameObject circlePrefabObject;

    // プレイヤーが選択したサークルオブジェクト
    [System.NonSerialized]
    public GameObject choiceCircleObject;

    // マウス稼働判定設定値
    private float _lineLength = 0.2f;

    // サークル記入フラグ
    [System.NonSerialized]
    public bool circleEntryFlag;

    // 円予測表示のトランスフォーム.
    [SerializeField]
    private Transform _circlePrediction;

    // 描画した円の開始位置
    [System.NonSerialized]
    public Vector2 startPos;

    // 描画した円の終了位置
    [System.NonSerialized]
    public Vector2 endPos;
    //親と子の処理判定
    [SerializeField]
    private string _processedSCENE;
    //Answer処理準備オブジェクト
    [SerializeField]
    private ChildAnswerPrepare _childAnswerPrepare;
    //親シーンの円開始位置
    private Vector2 dealerStartPos;
    //親シーンの円終了位置
    private Vector2 dealerEndPos;
    //マウス押下ポジション
    [System.NonSerialized]
    public Vector2 pushendPos;

    void Awake()
    {
        // 円予測表示の非表示
        _circlePrediction.gameObject.SetActive(false);
        // 決定ボタンとキャンセルボタンの非表示
        _decisionButtonObject.gameObject.SetActive(false);
        _cancelButtonObject.gameObject.SetActive(false);
        // 時間切れ表示オブジェクトの非表示
        _turnEndObject.gameObject.SetActive(false);
        // サークル記入フラグをオフにする
        circleEntryFlag = false;
    }

    void Start()
    {
        if (_processedSCENE == "ChildAnswer")
        {
            //引継ぎ情報を取得
            dealerStartPos = _childAnswerPrepare.startPos;
            dealerEndPos = _childAnswerPrepare.endPos;
        }
    }

    void Update()
    {
        // サークル記入フラグがfalseの場合のみ、記入が可能
        if (circleEntryFlag == false)
        {
            //稼働しているシーンで分岐
            if (_processedSCENE == "DealerAnswer")
            {
                DealeaAnswerScript();
            }
            else if (_processedSCENE == "ChildAnswer")
            {
                ChildAnswerScript();
            }

        }
    }

    private void DealeaAnswerScript()
    {
        // マウスが押下された時
        if (Input.GetMouseButtonDown(0))
        {
            // マウスが押された位置をstartPosに格納する。
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 円予測を表示.
            _circlePrediction.gameObject.SetActive(true);
        }
        // マウスが押下し続けられている間
        if (Input.GetMouseButton(0))
        {
            pushendPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // マウスのドラック量が設定値を超えた場合
            if ((pushendPos - startPos).magnitude > _lineLength)
            {
                // 円予測表示の表示位置を設定
                _circlePrediction.position = startPos;
                // 円予測表示のサイズを設定
                _circlePrediction.localScale = new Vector2(
                    (pushendPos - startPos).magnitude,
                    (pushendPos - startPos).magnitude
                );
            }
        }

        // マウスの押下が終了した時
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // マウスのドラック量が設定値を超えた場合
            if ((endPos - startPos).magnitude > _lineLength)
            {
                // startPosの位置を中心に円を配置する。
                choiceCircleObject = Instantiate(circlePrefabObject, startPos, Quaternion.identity);
                choiceCircleObject.transform.localScale = new Vector2(
                    (endPos - startPos).magnitude,
                    (endPos - startPos).magnitude
                );

                // startPosの位置をendPosに設定する
                // startPos = endPos;

                // 決定ボタンとキャンセルボタンを表示
                _decisionButtonObject.gameObject.SetActive(true);
                _cancelButtonObject.gameObject.SetActive(true);

                // 円予測表示の非表示
                _circlePrediction.gameObject.SetActive(false);

                // サークル記入フラグを有効にする
                circleEntryFlag = true;
            }
        }
    }

    private void ChildAnswerScript()
    {
        // マウスが押下された時
        if (Input.GetMouseButtonDown(0))
        {
            // マウスが押された位置をstartPosに格納する。
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 円予測を表示.
            _circlePrediction.gameObject.SetActive(true);
        }
        // マウスが押下し続けられている間
        if (Input.GetMouseButton(0))
        {
            pushendPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // マウスのドラック量が設定値を超えた場合
            if ((pushendPos - startPos).magnitude > _lineLength)
            {
                // 円予測表示の表示位置を設定
                _circlePrediction.position = pushendPos;
                // 円予測表示のサイズを設定
                _circlePrediction.localScale = new Vector2(
                    (dealerEndPos - dealerStartPos).magnitude,
                    (dealerEndPos - dealerStartPos).magnitude
                );
            }
        }
        // マウスの押下が終了した時
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // マウスのドラック量が設定値を超えた場合
            if ((endPos - startPos).magnitude > _lineLength)
            {
                // pushendPosの位置を中心に円を配置する。
                choiceCircleObject = Instantiate(circlePrefabObject, pushendPos, Quaternion.identity);
                choiceCircleObject.transform.localScale = new Vector2(
                    (dealerEndPos - dealerStartPos).magnitude,
                    (dealerEndPos - dealerStartPos).magnitude
                );

                // startPosの位置をendPosに設定する
                // startPos = endPos;

                // 決定ボタンとキャンセルボタンを表示
                _decisionButtonObject.gameObject.SetActive(true);
                _cancelButtonObject.gameObject.SetActive(true);

                // 円予測表示の非表示
                _circlePrediction.gameObject.SetActive(false);

                // サークル記入フラグを有効にする
                circleEntryFlag = true;
            }
        }

    }
}
