using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineCamera _followCamera;


    private void Awake()
    {
        _followCamera = GetComponent<CinemachineCamera>();

        EventBus<AreaEntered>.OnEvent += HandleAreaEntered;
    }

    private void OnDestroy()
    {
        EventBus<AreaEntered>.OnEvent -= HandleAreaEntered;
    }


    private void HandleAreaEntered(AreaEntered @event)
    {
        Debug.Log("Area Entered - Setting Follow Target");
        _followCamera.Target.TrackingTarget = PlayerController.Instance.transform;
    }
}
