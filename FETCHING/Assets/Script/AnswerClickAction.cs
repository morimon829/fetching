using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerClickAction : MonoBehaviour
{
    public GameObject linePrefab;
    private Vector2 startPos;
    private float lineLength = 0.2f;
    private float lineWidth = 5.0f;
    // プレイヤーが選択したサークル
    public GameObject choiceCircle;
    //サークル記入フラグ
    public bool circleEntryFlag;
    // 円予測表示のトランスフォーム.
    private Transform CirclePrediction = null;
    //決定ボタン
    private GameObject decisionButton;
    //キャンセルボタン
    private GameObject cancelButton;

    // Start is called before the first frame update
    void Start()
    {
        // 決定ボタンのオブジェクトを取得
        decisionButton = GameObject.Find("DecisionButton");
        // キャンセルボタンのオブジェクトを取得
        cancelButton = GameObject.Find("CancelButton");
        // 円予測のトランスフォームを取得
        CirclePrediction = transform.Find("CirclePrediction");

        // 円予測表示の非表示
        CirclePrediction.gameObject.SetActive( false );
        // 決定ボタンとキャンセルボタンの非表示
        decisionButton.gameObject.SetActive( false );
        cancelButton.gameObject.SetActive( false );
        //サークル記入フラグをオフにする
        circleEntryFlag = false;
        
    }

    // Update is called once per frame
    void Update()
    {
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
                    choiceCircle = Instantiate(linePrefab, startPos, Quaternion.identity);
                    choiceCircle.transform.localScale = new Vector2 ((endPos - startPos).magnitude ,(endPos - startPos).magnitude);

                    //startPosの位置をendPosに設定する
                    startPos = endPos;

                    // 決定ボタンとキャンセルボタンを表示
                    decisionButton.gameObject.SetActive( true );
                    cancelButton.gameObject.SetActive( true );

                    //サークル記入フラグを有効にする
                    circleEntryFlag = true;
                    
                }
                
            }
        }
    }
}
