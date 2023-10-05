using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using TMPro;

public class AnnouncementOfResults : MonoBehaviour
{
    //基準スコア
    [SerializeField]
    private int _baseScore;

    //Answer処理準備オブジェクト
    [SerializeField]
    private GameResultPrepare _gameResultPrepare;
    // 親プレイヤーが選択したサークルオブジェクト
    [System.NonSerialized]
    public GameObject player1CircleObject;
    // 子プレイヤーが選択したサークルオブジェクト
    [System.NonSerialized]
    public GameObject player2CircleObject;
    [System.NonSerialized]
    public GameObject player3CircleObject;
    [System.NonSerialized]
    public GameObject player4CircleObject;

    // サークルプレハブオブジェクト
    public GameObject CirclePrefabObject;
    //リザルトボード
    [SerializeField]
    private GameObject _resultBord;
    //リザルト(文字)
    private GameObject _resultBordWord;
    //プレイヤーカラー
    [SerializeField]
    private Material _player1Caler;

    [SerializeField]
    private Material _player2Caler;
    [SerializeField]
    private Material _player3Caler;

    [SerializeField]
    //プレイヤースコア
    private Material _player4Caler;
    [System.NonSerialized]
    private int _player1Score;
    [System.NonSerialized]
    private int _player2Score;
    [System.NonSerialized]
    private int _player3Score;
    [System.NonSerialized]
    private int _player4Score;

    //スコアデータ
    [SerializeField]
    private GameObject _scoreData;

    //スコアリスト設定処理
    [SerializeField]
    private SetScoreList _setScoreList;

    //画面表示スコアデータ(左下)
    [SerializeField]
    private GameObject _displayScoreList;

    //受け渡し用スコアリスト
    [System.NonSerialized]
    public int[] passingScoreList;


    void Start()
    {

        //リザルトボードを非表示に
        // _resultBord.SetActive(false);

        //円の描画
        player1CircleObject = ChildCircleDraw(_gameResultPrepare.DealerStartPos, _player1Caler);
        player2CircleObject = ChildCircleDraw(_gameResultPrepare.ChildPushendPos, _player2Caler);
        player3CircleObject = ChildCircleDraw(new Vector2(956, 400), _player3Caler);
        player4CircleObject = ChildCircleDraw(new Vector2(656, 500), _player4Caler);

        //現在のスコアをスコアリストに描画
        passingScoreList = _setScoreList.SettingScoreList(_displayScoreList, _gameResultPrepare.ScoreList);

        //ポイントリストを作成する。
        int[] pointlist = new int[4];
        pointlist[0] = 0;
        //プレイヤー2
        //点数算出処理
        pointlist[1] = PointCalculation(player1CircleObject, player2CircleObject);

        //プレイヤー3
        pointlist[2] = PointCalculation(player1CircleObject, player3CircleObject);

        //プレイヤー4
        pointlist[3] = PointCalculation(player1CircleObject, player4CircleObject);

        //リザルトボードを表示
        _resultBord.SetActive(true);

        // スコアを画面のテキストに反映する
        int count = 0;
        foreach (TextMeshProUGUI scoreDatas in _scoreData.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (scoreDatas.name == "Score")
            {
                int nowscore = passingScoreList[count] + pointlist[count];
                scoreDatas.text = nowscore.ToString();
                passingScoreList[count] = nowscore;
                count++;
            }
        }
    }

    // 円を描画
    // 引数のcenterPositionは親は「_gameResultPrepare.DealerStartPos」、子は「_gameResultPrepare.ChildPushendPos」を渡す
    private GameObject ChildCircleDraw(Vector2 centerPosition, Material playerCaler)
    {
        GameObject ChildChoiceCircleObject = Instantiate(CirclePrefabObject, centerPosition, Quaternion.identity);
        ChildChoiceCircleObject.transform.localScale = new Vector2(
            (_gameResultPrepare.DealerEndPos - _gameResultPrepare.DealerStartPos).magnitude,
            (_gameResultPrepare.DealerEndPos - _gameResultPrepare.DealerStartPos).magnitude
        );
        ChildChoiceCircleObject.GetComponent<Renderer>().material = playerCaler;

        return ChildChoiceCircleObject;
    }

    //点数(親と子の中心点の距離)算出処理
    private int PointCalculation(GameObject Dealer, GameObject Child)
    {
        float DealerX = Math.Abs(Dealer.transform.position.x);
        float DealerY = Math.Abs(Dealer.transform.position.y);
        float ChildX = Math.Abs(Child.transform.position.x);
        float ChildY = Math.Abs(Child.transform.position.y);

        int distance = (int)Math.Sqrt((Math.Pow(DealerX - ChildX, 2) + Math.Pow(DealerY - ChildY, 2)));
        //距離-基準スコア=点数
        int Reset = _baseScore - distance;
        if (0 > Reset)
        {
            Reset = 0;
        }

        return Reset;

    }

}
