using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCoordinator : Singleton<SceneCoordinator>
{
    public enum SceneIndex
    {
        Scene1 = 0,
        Scene2 = 1,
        Loading = 2
    }

    private SceneIndex _targetSceneIndex;
    public string SceneTransitionName { get; private set; } = string.Empty;


    protected override void Awake()
    {
        base.Awake();
        EventBus<AreaEntered>.OnEvent += HandleAreaEntered;
    }

    private void OnDestroy()
    {
        EventBus<AreaEntered>.OnEvent -= HandleAreaEntered;
    }


    private void HandleAreaEntered(AreaEntered @event)
    {
        SceneTransition.Instance.FadeToClear();
    }


    public void LoadSceneCallback()
    {
        StartCoroutine(LoadSceneRoutine(_targetSceneIndex));
    }

    public void LoadScene(SceneIndex sceneIndex, string sceneTransitionName, bool direct = true)
    {
        _targetSceneIndex = sceneIndex;
        SceneTransitionName = sceneTransitionName;

        SceneTransition.Instance.FadeToBlack();

        if (direct)
        {
            LoadSceneCallback();
            return;
        }
        
        SceneManager.LoadScene((int)SceneIndex.Loading);
    }


    private IEnumerator LoadSceneRoutine(SceneIndex sceneIndex)
    {
        yield return new WaitForSeconds(SceneTransition.Instance.FadeDuration);
        SceneManager.LoadScene((int)_targetSceneIndex);
    }
}
