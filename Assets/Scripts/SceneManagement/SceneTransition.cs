using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : Singleton<SceneTransition>
{
    [SerializeField] private Image fadeScreen;
    [field: SerializeField] public float FadeDuration { get; private set; } = 1f;

    public void FadeToBlack() => ExecuteTransition(1f);
    
    public void FadeToClear() => ExecuteTransition(0f);


    private void ExecuteTransition(float targetAlpha)
    {
        StopAllCoroutines();
        StartCoroutine(FadeRoutine(targetAlpha));
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, FadeDuration * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
    }
}
