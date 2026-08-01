using UnityEngine;

public class Loader : MonoBehaviour
{
    private void Update()
    {
        SceneCoordinator.Instance.LoadSceneCallback();
        gameObject.SetActive(false);
    }
}
