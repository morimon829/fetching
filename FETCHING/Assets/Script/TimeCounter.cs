using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public float countdownSeconds;
    private TextMeshProUGUI timeText;
    //timeCounterscriptの情報
    private TurnEndScript turnEndScript;
    //カウントダウン終了フラグ
    public bool countDownEndflg;

    private void Start()
    {
        //カウントダウン終了フラグをfalseに
        countDownEndflg = false;
        //テキストのコンポーネント情報を取得
        timeText = this.GetComponent<TextMeshProUGUI>();
        //TurnEndScriptの情報を取得
        turnEndScript = GameObject.Find("DecisionButton").GetComponent<TurnEndScript>();
    }

    void Update()
    {
        //カウントダウン終了フラグが有効でない場合のみ処理を行う
        if (countDownEndflg == false)
        {
            if (0 <= countdownSeconds)
            {
                //設定時間-経過時間
                countdownSeconds -= Time.deltaTime;
                //上記の計算結果を文字列に修正して、テキストに反映
                timeText.text = countdownSeconds.ToString("f0");
            }

            if (countdownSeconds <= 0)
            {
                //カウントダウン終了フラグをtrueに
                countDownEndflg = true;
                // 0秒になったときの処理
                turnEndScript.turnEnd();
            }
        }
    }
}