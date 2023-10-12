using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultScorelistStartProcessing : MonoBehaviour
{
    //Answer処理準備オブジェクト
    [SerializeField]
    private GameResultPrepare _gameResultPrepare;

    //スコアリスト設定処理
    [SerializeField]
    private SetScoreList _setScoreList;

    //スコアデータ
    [SerializeField]
    private GameObject _displayScoreList;

    //受け渡し用スコアリスト
    [System.NonSerialized]
    public int[] passingScoreList;

    void Start()
    {
        passingScoreList = _setScoreList.SettingScoreList(_displayScoreList, _gameResultPrepare.ScoreList);
    }

}
