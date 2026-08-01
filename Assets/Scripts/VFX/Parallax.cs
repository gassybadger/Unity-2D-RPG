using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _parallaxOffset = -.15f;

    
    private Vector2 _startPosition = Vector2.zero;
    private Vector3 _lastCameraPosition;


    private void Start()
    {
        _lastCameraPosition = Camera.main.transform.position;
    }

    private void FixedUpdate()
    {
        // Calculate how much the camera moved during this frame
        Vector3 deltaMovement = Camera.main.transform.position - _lastCameraPosition;

        // Apply parallax movement to the canopy/background object
        // Multiplied by parallaxAmount to create the depth effect
        transform.position += deltaMovement * _parallaxOffset;

        // Save the current camera position for the next frame's calculation
        _lastCameraPosition = Camera.main.transform.position;
    }
}
