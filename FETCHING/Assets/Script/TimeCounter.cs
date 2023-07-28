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

    private void Start()
    {
        timeText = this.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        countdownSeconds -= Time.deltaTime;
        timeText.text = countdownSeconds.ToString("f0");

        if (countdownSeconds <= 0)
        {
            // 0秒になったときの処理
        }
    }
}