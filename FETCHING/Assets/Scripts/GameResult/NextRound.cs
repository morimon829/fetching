using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRound : MonoBehaviour
{
    [SerializeField]
    private AnnouncementOfResults _announcementOfResults;
    public void OnBottan()
    {
        // イベントに登録
        SceneManager.sceneLoaded += ImageLoadGameSceneLoaded;
        // シーン切り替え
        SceneManager.LoadScene("ImageLoad");

    }

    private void ImageLoadGameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        ImageLoadPrepare imageLoadPrepare = GameObject.FindWithTag("GameManager").GetComponent<ImageLoadPrepare>();

        // // データを渡す処理
        imageLoadPrepare.ScoreList = _announcementOfResults.passingScoreList;

        // イベントから削除
        SceneManager.sceneLoaded -= ImageLoadGameSceneLoaded;
    }
}
