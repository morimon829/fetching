using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public GameObject linePrefab;
    private Vector2 startPos;
    // 円予測表示のトランスフォーム.
    [SerializeField] Transform CirclePrediction = null;
    private float lineLength = 0.2f;
    private float lineWidth = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
         // 円予測表示の非表示
        CirclePrediction.gameObject.SetActive( false );
    }

    // Update is called once per frame
    void Update()
    {
        //マウスが押下された時
        if (Input.GetMouseButtonDown(0))
        {
            //マウスが押された位置をstartPosに格納する。
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 方向線を表示.
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
                GameObject obj = Instantiate(linePrefab, startPos, Quaternion.identity);
                obj.transform.localScale = new Vector2 ((endPos - startPos).magnitude ,(endPos - startPos).magnitude);

                //startPosの位置をendPosに設定する
                startPos = endPos;
            }
            
        }
    }
}
