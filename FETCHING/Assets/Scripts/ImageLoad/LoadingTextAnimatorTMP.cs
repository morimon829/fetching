using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingTextAnimatorTMP : MonoBehaviour
{
    public TMP_Text loadingText;

    private string baseText = "ふぇち抽出中";
    private bool isAnimating;

    private void Start()
    {
        StartAnimating();
    }

    public void StartAnimating()
    {
        isAnimating = true;
        StartCoroutine(AnimateLoadingText());
    }

    public void StopAnimating()
    {
        isAnimating = false;
        loadingText.text = baseText;
    }

    public IEnumerator AnimateLoadingText()
    {
        while (isAnimating)
        {
            loadingText.text = baseText;
            for (int i = 0; i < 3; i++)
            {
                if (!isAnimating) yield break;
                loadingText.text += ".";
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}