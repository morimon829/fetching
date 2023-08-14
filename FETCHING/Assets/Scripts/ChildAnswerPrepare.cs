using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildAnswerPrepare : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;

    void Start()
    {
        Debug.Log("シーン切り替え後");
        Debug.Log(startPos);
    }
}
