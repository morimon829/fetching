using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultPrepare : MonoBehaviour
{
    //親のサークル情報
    [System.NonSerialized]
    public Vector2 DealerStartPos;
    [System.NonSerialized]
    public Vector2 DealerEndPos;
    [System.NonSerialized]
    public Vector2 DealerPushendPos;

    //子のサークル情報
    [System.NonSerialized]
    public Vector2 ChildStartPos;
    [System.NonSerialized]
    public Vector2 ChildEndPos;
    [System.NonSerialized]
    public Vector2 ChildPushendPos;

    void Start()
    {
        // Debug.Log(DealerStartPos);
        // Debug.Log(DealerEndPos);
        // Debug.Log(DealerPushendPos);
        // Debug.Log(ChildStartPos);
        // Debug.Log(ChildEndPos);
        // Debug.Log(ChildPushendPos);
    }
}
