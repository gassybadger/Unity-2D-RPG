using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rigidBody;
    private Vector2 targetPosition;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + targetPosition * (moveSpeed * Time.fixedDeltaTime));
    }


    public void MoveTo(Vector2 position)
    {
        targetPosition = position;
    }
}
