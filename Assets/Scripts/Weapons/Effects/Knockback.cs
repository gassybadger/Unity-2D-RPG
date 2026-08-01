using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Knockback : MonoBehaviour
{
    private float knockbackRecoveryTime = .25f;

    private Rigidbody2D rigidBody;
    private float knockbackTime = 0;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }


    private void LateUpdate()
    {
        if (Time.time - knockbackTime > knockbackRecoveryTime)
        {
            rigidBody.linearVelocity = Vector2.zero;
            if (transform.TryGetComponent(out EnemyPathfinding pathfinding))
            {
                pathfinding.enabled = true;
            }
        }
    }

    public void ApplyKnockbackForce(Transform source, float knockbackAmount)
    {
        if (transform.TryGetComponent(out EnemyPathfinding pathfinding))
        {
            pathfinding.enabled = false;
        }

        Vector2 force = (transform.position - source.position).normalized * knockbackAmount * rigidBody.mass;
        
        rigidBody.AddForce(force, ForceMode2D.Impulse);
        knockbackTime = Time.time;
    }
}
