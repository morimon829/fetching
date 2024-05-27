using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputLobbyId : MonoBehaviour
{
    //フェードパネルの取得
    [SerializeField]
    private GameObject _panelfade;
    //roomID入力欄の取得
    [SerializeField]
    private GameObject _roomIdField;
    //検索オブジェクトの取得
    [SerializeField]
    private GameObject _roomSearch;
    //戻るボタンオブジェクトの取得
    [SerializeField]
    private GameObject _return;
    //ルームIDボックス注意メッセージ
    [SerializeField]
    private GameObject _roomIdAlert;

    //フェードパネルのイメージ取得変数
    private Image _fadealpha;
    //パネルのalpha値取得変数
    private float _transparency;
    //フェードアウトのフラグ変数
    private bool _fadeoutflg;
    //フェードインのフラグ変数
    private bool _fadeinflg;
    //フェードイン・アウトスピード
    private float _feedspeed = 0.005f;

    void Start()
    {
        //パネル配下のオブジェクトの非アクティブ
        _panelfade.gameObject.SetActive(false);
        _roomIdField.gameObject.SetActive(false);
        _roomSearch.gameObject.SetActive(false);
        _return.gameObject.SetActive(false);

        //パネルのイメージ取得
        _fadealpha = _panelfade.GetComponent<Image>();
        //パネルのalpha値を取得
        _transparency = _fadealpha.color.a;
    }

    void Update()
    {
        if (_fadeoutflg == true)
        {
            FadeOut();
        }
        else if (_fadeinflg == true)
        {
            FadeIn();
        }
    }

    public void PushEnteringRoomButton()
    {
        _panelfade.gameObject.SetActive(true);
        _fadeoutflg = true;
    }
    public void PushReturnButton()
    {
        _roomIdField.gameObject.SetActive(false);
        _roomSearch.gameObject.SetActive(false);
        _return.gameObject.SetActive(false);
        _roomIdAlert.gameObject.SetActive(false);
        _fadeinflg = true;
    }

    void FadeOut()
    {
        _transparency += _feedspeed;
        _fadealpha.color = new Color(0, 0, 0, _transparency);
        if (_transparency >= 0.95)
        {
            _fadeoutflg = false;
            _roomIdField.gameObject.SetActive(true);
            _roomSearch.gameObject.SetActive(true);
            _return.gameObject.SetActive(true);
        }
    }
    void FadeIn()
    {
        _transparency -= _feedspeed;
        _fadealpha.color = new Color(0, 0, 0, _transparency);
        if (0 >= _transparency)
        {
            _fadeinflg = false;
            _panelfade.gameObject.SetActive(false);
        }
    }
}