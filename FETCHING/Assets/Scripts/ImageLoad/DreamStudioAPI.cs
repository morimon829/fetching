using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;

public class DreamStudioAPI : MonoBehaviour
{
    //画像生成APIへの問い合わせ情報
    private string apiUrl = "https://api.stability.ai/v1/generation/stable-diffusion-xl-beta-v2-2-2/text-to-image";
    private string apiKey = "sk-hSboQXiCSTnPPLUS0qCuVRziWjY2Vl1JlldJyG3YJ3LqJsc5";    //キーワード入力欄の取得

    //画像生成キーワード
    [SerializeField]
    private TMP_InputField _keyWordInputField;
    //画像生成先image
    [SerializeField]
    private Image _displayImage;
    //キーワード入力注意メッセージ
    [SerializeField]
    private GameObject _InputKeyWardAlert;

    //読み込み中(API問い合わせ中)表示処理関係
    //読み込み時メッセージ
    [SerializeField]
    private TMP_Text _loadingTMPText;
    //読み込み時パネル
    [SerializeField]
    private GameObject _loadingPanel;
    //ロードアニメーション設定クラス
    private LoadingTextAnimatorTMP loadingTextAnimatorTMP;

    //フェードアウト関係のオブジェクト(読み込み終了時にオブジェクトの非活性にする為に呼びだし)
    //フェードパネルの取得
    [SerializeField]
    private GameObject _panelfade;
    //キーワード入力欄の取得
    [SerializeField]
    private GameObject _keywordField;
    //画像生成開始ボタンオブジェクトの取得
    [SerializeField]
    private GameObject _imageGenerateButton;
    //戻るボタンオブジェクトの取得
    [SerializeField]
    private GameObject _return;

    //フェードアウト関係の設定値
    //フェードアウトのフラグ変数
    private bool _fadeoutflg;
    //フェードイン・アウトスピード
    private float _feedspeed = 0.3f;
    //パネルのalpha値取得変数
    private float _transparency;
    //フェードパネルのイメージ取得変数
    private Image _fadealpha;

    void Start()
    {
        //オブジェクトを非表示
        _InputKeyWardAlert.gameObject.SetActive(false);
        _loadingTMPText.gameObject.SetActive(false);
        _loadingPanel.gameObject.SetActive(false);
        loadingTextAnimatorTMP = _loadingTMPText.GetComponent<LoadingTextAnimatorTMP>();
        //API問い合わせ中パネルのイメージ取得
        _fadealpha = _loadingPanel.GetComponent<Image>();
        //パネルのalpha値を取得
        _transparency = _fadealpha.color.a;
    }

    void Update()
    {
        if (_fadeoutflg == true)
        {
            FadeOut();
        }
    }

    public void OnGenerateButtonClick()
    {
        //入力チェック
        if (string.IsNullOrEmpty(_keyWordInputField.text))
        {
            _InputKeyWardAlert.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(RequestImage(_keyWordInputField.text));
        }
    }

    IEnumerator RequestImage(string prompt)
    {
        //画像生成APIへPOSTする情報
        var requestBody = new
        {
            text_prompts = new[] { new { text = prompt + ",Japanese Animation", weight = 1 } },
            cfg_scale = 7,
            height = 512,
            width = 768,
            sampler = "K_DPM_2_ANCESTRAL",
            style_scale = 1.5f,
            style_guidance_scale = 1.0f,
            noise_scale = 0.2f,
            samples = 1,
            steps = 10
        };
        //ロードアニメーション関係のオブジェクトを活性にする
        _loadingTMPText.gameObject.SetActive(true);
        _loadingPanel.gameObject.SetActive(true);

        // アニメーションをアクティブにする
        StartCoroutine(loadingTextAnimatorTMP.AnimateLoadingText());

        string jsonBody = JsonConvert.SerializeObject(requestBody);
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonBody);

        UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "POST");
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            byte[] imageData = webRequest.downloadHandler.data;
            string jsonResponse = System.Text.Encoding.UTF8.GetString(imageData);
            ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
            string base64Image = response.artifacts[0].base64;
            byte[] imageBytes = System.Convert.FromBase64String(base64Image);

            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            // Texture2D から Sprite を作成
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Image の sprite プロパティに設定
            _displayImage.sprite = sprite;
        }
        else
        {
            Debug.LogError($"Image generation request failed: {webRequest.error}, Response Code: {webRequest.responseCode}, URL: {apiUrl}, Response: {webRequest.downloadHandler.text}");
        }
        //ロード画面オブジェクトをフェードアウトさせる
        _fadeoutflg = true;

    }

    [System.Serializable]
    public class ApiResponse
    {
        public Artifact[] artifacts;

        [System.Serializable]
        public class Artifact
        {
            public string base64;
        }
    }

    void FadeOut()
    {
        _transparency -= _feedspeed;
        _fadealpha.color = new Color(0, 0, 0, _transparency);
        if (0 >= _transparency)
        {
            _fadeoutflg = false;
            _loadingPanel.gameObject.SetActive(false);
            _loadingTMPText.gameObject.SetActive(false);
            // アニメーションを非アクティブにする
            StopCoroutine(loadingTextAnimatorTMP.AnimateLoadingText());

            //API問い合わせ情報入力欄をすべて非活性にする
            _keywordField.gameObject.SetActive(false);
            _imageGenerateButton.gameObject.SetActive(false);
            _return.gameObject.SetActive(false);
            _panelfade.gameObject.SetActive(false);
        }
    }
}
