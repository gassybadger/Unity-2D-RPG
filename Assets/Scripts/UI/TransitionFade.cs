using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class TransitionFade 
    : Singleton<TransitionFade>
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration;

    public void FadeToBlack()
    {
        StopAllCoroutines();
        StartCoroutine(FadeSceneRoutine(1f));
    }

    public void FadeToClear()
    {
        StopAllCoroutines();
        StartCoroutine(FadeSceneRoutine(0f));
    }


    private IEnumerator FadeSceneRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(_fadeImage.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(_fadeImage.color.a, targetAlpha, _fadeDuration * Time.deltaTime);
            _fadeImage.color = new Color(
                _fadeImage.color.r,
                _fadeImage.color.g,
                _fadeImage.color.b,
                alpha
            );
            yield return null;
        }
    }

}
