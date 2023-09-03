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
        //親の円を描画
        // startPosの位置を中心に円を配置する。
        DealerChoiceCircleObject = Instantiate(CirclePrefabObject, _gameResultPrepare.DealerStartPos, Quaternion.identity);
        DealerChoiceCircleObject.transform.localScale = new Vector2(
            (_gameResultPrepare.DealerEndPos - _gameResultPrepare.DealerStartPos).magnitude,
            (_gameResultPrepare.DealerEndPos - _gameResultPrepare.DealerStartPos).magnitude
        );
        DealerChoiceCircleObject.GetComponent<Renderer>().material = player1Caler;

        //子の円を描画
        // PushendPosの位置を中心に円を配置する。
        ChildChoiceCircleObject = Instantiate(CirclePrefabObject, _gameResultPrepare.ChildPushendPos, Quaternion.identity);
        ChildChoiceCircleObject.transform.localScale = new Vector2(
            (_gameResultPrepare.DealerEndPos - _gameResultPrepare.DealerStartPos).magnitude,
            (_gameResultPrepare.DealerEndPos - _gameResultPrepare.DealerStartPos).magnitude
        );
        ChildChoiceCircleObject.GetComponent<Renderer>().material = player2Caler;

        float DealerX = Math.Abs(DealerChoiceCircleObject.transform.position.x);
        float DealerY = Math.Abs(DealerChoiceCircleObject.transform.position.y);
        float ChildX = Math.Abs(ChildChoiceCircleObject.transform.position.x);
        float ChildY = Math.Abs(ChildChoiceCircleObject.transform.position.y);

        var Reset = Math.Sqrt((Math.Pow(DealerX - ChildX, 2) + Math.Pow(DealerY - ChildY, 2)));
        Debug.Log(Reset);
    }
}