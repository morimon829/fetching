using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementOfResults : MonoBehaviour
{
    //Answer処理準備オブジェクト
    [SerializeField]
    private GameResultPrepare _gameResultPrepare;
    // 親プレイヤーが選択したサークルオブジェクト
    [System.NonSerialized]
    public GameObject DealerChoiceCircleObject;
    // 子プレイヤーが選択したサークルオブジェクト
    [System.NonSerialized]
    public GameObject ChildChoiceCircleObject;
    // サークルプレハブオブジェクト
    public GameObject CirclePrefabObject;

    [SerializeField]
    private Material player1Caler;

    [SerializeField]
    private Material player2Caler;

    void Start()
    {

        // 親の円を描画
        DealerChoiceCircleObject = ParentCircleDraw();

        //子の円を描画
        //プレイヤー1
        ChildChoiceCircleObject = ChildCircleDraw(_gameResultPrepare.ChildPushendPos, player2Caler);

        //点数算出処理
        PointCalculation(DealerChoiceCircleObject, ChildChoiceCircleObject);

        //結果発表

    }

    //親の円描画処理
    // startPosの位置を中心に円を配置する。
    private GameObject ParentCircleDraw()
    {
        DealerChoiceCircleObject = Instantiate(CirclePrefabObject, _gameResultPrepare.DealerStartPos, Quaternion.identity);
        DealerChoiceCircleObject.transform.localScale = new Vector2(
            (_gameResultPrepare.DealerEndPos - _gameResultPrepare.DealerStartPos).magnitude,
            (_gameResultPrepare.DealerEndPos - _gameResultPrepare.DealerStartPos).magnitude
        );
        DealerChoiceCircleObject.GetComponent<Renderer>().material = player1Caler;

        return DealerChoiceCircleObject;
    }

    // 子の円を描画
    // PushendPosの位置を中心に円を配置する。
    private GameObject ChildCircleDraw(Vector2 PushendPos, Material playerCaler)
    {
        ChildChoiceCircleObject = Instantiate(CirclePrefabObject, PushendPos, Quaternion.identity);
        ChildChoiceCircleObject.transform.localScale = new Vector2(
            (_gameResultPrepare.DealerEndPos - _gameResultPrepare.DealerStartPos).magnitude,
            (_gameResultPrepare.DealerEndPos - _gameResultPrepare.DealerStartPos).magnitude
        );
        ChildChoiceCircleObject.GetComponent<Renderer>().material = playerCaler;

        return ChildChoiceCircleObject;
    }





    //点数(親と子の中心点の距離)算出処理
    private void PointCalculation(GameObject Dealer, GameObject Child)
    {
        float DealerX = Math.Abs(Dealer.transform.position.x);
        float DealerY = Math.Abs(Dealer.transform.position.y);
        float ChildX = Math.Abs(Child.transform.position.x);
        float ChildY = Math.Abs(Child.transform.position.y);

        var Reset = Math.Sqrt((Math.Pow(DealerX - ChildX, 2) + Math.Pow(DealerY - ChildY, 2)));
        Debug.Log(Reset);
    }

}