
using UnityEngine;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private SceneCoordinator.SceneIndex _sceneToLoad;
    [SerializeField] private string _sceneTransitionName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController _))
        {
            SceneCoordinator.Instance.LoadScene(_sceneToLoad, _sceneTransitionName);
        }
    }
}