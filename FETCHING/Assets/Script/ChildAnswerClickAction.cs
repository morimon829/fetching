using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAnswerClickAction : MonoBehaviour
{
    public GameObject circlePrefab;
    public Vector2 startPos;
    public Vector2 endPos;
    private float lineLength = 0.2f;
    private float lineWidth = 5.0f;
    // プレイヤーが選択したサークル
    public GameObject choiceCircle;
    //サークル記入フラグ
    public bool circleEntryFlag;
    // 円予測表示のトランスフォーム.
    private Transform CirclePrediction = null;
    //  時間切れ表示
    private GameObject turnEnd;
    //決定ボタン
    private GameObject decisionButton;
    //キャンセルボタン
    private GameObject cancelButton;
    //ChildAnswer準備クラス
    private ChildAnswerPrepare childAnswerPrepare; 

    // Start is called before the first frame update
    void Start()
    {
        // 前のシーンから受け渡された情報を取得
        childAnswerPrepare = GameObject.Find("ChildAnswerPrepare").GetComponent<ChildAnswerPrepare>();

        // 決定ボタンのオブジェクトを取得
        decisionButton = GameObject.Find("DecisionButton");
        // キャンセルボタンのオブジェクトを取得
        cancelButton = GameObject.Find("CancelButton");
        // 円予測のトランスフォームを取得
        CirclePrediction = transform.Find("CirclePrediction");
        // 時間切れ表示オブジェクトの情報を取得
        turnEnd = GameObject.Find("TurnEnd");

        // 円予測表示の非表示
        CirclePrediction.gameObject.SetActive( false );
        // 決定ボタンとキャンセルボタンの非表示
        decisionButton.gameObject.SetActive( false );
        cancelButton.gameObject.SetActive( false );
        // 時間切れ表示オブジェクトの非表示
        turnEnd.gameObject.SetActive( false );
        // サークル記入フラグをオフにする
        circleEntryFlag = false;
        
    }

    void Update()
    {
        //サークル記入フラグがfalseの場合のみ、記入が可能
        if (circleEntryFlag == false)
        {
            //マウスが押下された時
            if (Input.GetMouseButtonDown(0))
            {
                //マウスが押された位置をstartPosに格納する。
                startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // 円予測を表示.
                CirclePrediction.gameObject.SetActive( true );
            }
            //マウスが押下し続けられている間
            if (Input.GetMouseButton(0))
            {
                Vector2 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //マウスのドラック量が0.2を超えた場合
                if ((endPos - startPos).magnitude > lineLength)
                {
                    //円予測表示の表示位置を設定
                    CirclePrediction.position = startPos;
                    //円予測表示のサイズを設定
                    CirclePrediction.localScale = new Vector2 ((endPos - startPos).magnitude ,(endPos - startPos).magnitude);
                }
            }

            //マウスの押下が終了した時
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //マウスのドラック量が0.2を超えた場合
                if ((endPos - startPos).magnitude > lineLength)
                {
                    //startPosの位置を中心に円を配置する。
                    choiceCircle = Instantiate(circlePrefab, startPos, Quaternion.identity);
                    choiceCircle.transform.localScale = new Vector2 ((endPos - startPos).magnitude ,(endPos - startPos).magnitude);

                    //startPosの位置をendPosに設定する
                    startPos = endPos;

                    // 決定ボタンとキャンセルボタンを表示
                    decisionButton.gameObject.SetActive( true );
                    cancelButton.gameObject.SetActive( true );

                    // 円予測表示の非表示
                    CirclePrediction.gameObject.SetActive( false );

                    //サークル記入フラグを有効にする
                    circleEntryFlag = true;
                    
                }
            }
        }
    }
}
