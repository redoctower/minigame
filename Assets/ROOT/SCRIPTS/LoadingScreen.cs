using System.Collections;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private CanvasGroup loadingScreen;

    private void Awake()
    {
        loadingScreen = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeImage(false, loadingScreen, 5f));
    }
    public void FadeOut()
    {
        StartCoroutine(FadeImage(true, loadingScreen, 5f));
    }

    IEnumerator FadeImage(bool fadeAway, CanvasGroup canvasGroup, float duration)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime * duration)
            {
                canvasGroup.alpha = i;
                yield return null;
            }
            gameObject.SetActive(false);
        }
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime * duration)
            {
                canvasGroup.alpha = i;
                yield return null;
            }
        }
    }
}
