using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetScoreList : MonoBehaviour
{

    public int[] SettingScoreList(GameObject displayScoreList, int[] nowScoreList)
    {
        //スコアリストがNullの場合、スコアリストを作成する(ゲーム開始直後か判定)
        if (nowScoreList == null || nowScoreList.Length == 0)
        {
            nowScoreList = new int[4];

            //0を設定する
            int listcout = 0;
            foreach (int i in nowScoreList)
            {
                nowScoreList[listcout] = 0;
                listcout++;
            }
        }
        //画面に反映する
        int listsetcout = 0;
        foreach (TextMeshProUGUI scoreDatas in displayScoreList.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (scoreDatas.name == "Score")
            {
                scoreDatas.text = nowScoreList[listsetcout].ToString();
                listsetcout++;
            }
        }

        return nowScoreList;
    }



}
