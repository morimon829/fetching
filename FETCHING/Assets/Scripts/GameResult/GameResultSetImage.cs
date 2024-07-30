using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameResultSetImage : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private GameResultPrepare _prepareObject;

    void Start()
    {
        _image.sprite = _prepareObject.CreatedSprite;
        _image.color = new Color(1, 1, 1, 1);
        _image.preserveAspect = true;
    }
}
