
using Unity.Cinemachine;
using UnityEngine;

public class AreaEntrance : MonoBehaviour 
{
    [SerializeField] private string _transitionName;


    private void Start()
    {
        if (SceneCoordinator.Instance?.SceneTransitionName == _transitionName)
        {
            PlayerController.Instance.transform.position = transform.position;

            EventBus<AreaEntered>.Raise(new AreaEntered());
        }
    }
}