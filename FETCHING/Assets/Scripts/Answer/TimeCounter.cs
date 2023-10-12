using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    // カウントダウン終了フラグ
    [System.NonSerialized]
    public bool countDownEndflg;
    // 時間表示領域
    [SerializeField]
    private TextMeshProUGUI _timeText;
    // timeCounterscriptの情報
    [SerializeField]
    private TurnEndScript _turnEndScript;
    // カウントダウン時間
    public float countdownSeconds;

    private void Awake()
    {
        // カウントダウン終了フラグをfalseに
        countDownEndflg = false;
    }

    void Update()
    {
        // カウントダウン終了フラグが有効でない場合のみ処理を行う
        if (countDownEndflg == false)
        {
            if (0 <= countdownSeconds)
            {
                // 設定時間-経過時間
                countdownSeconds -= Time.deltaTime;
                // 上記の計算結果を文字列に修正して、テキストに反映
                _timeText.text = countdownSeconds.ToString("f0");
            }

            if (countdownSeconds <= 0)
            {
                // カウントダウン終了フラグをtrueに
                countDownEndflg = true;
                // 0秒になったときの処理
                _turnEndScript.TurnEnd();
            }
        }
    }
}