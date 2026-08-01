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
            //rigidBody.linearVelocity = Vector2.zero;
            if (transform.TryGetComponent(out EnemyPathfinding pathfinding))
            {
                pathfinding.enabled = true;
            }
        }
    }

    public void ApplyKnockbackForce(Vector3 source, float knockbackAmount)
    {
        if (transform.TryGetComponent(out EnemyPathfinding pathfinding))
        {
            pathfinding.enabled = false;
        }


        Vector2 normalizedDirection = (transform.position - source).normalized;

        Vector2 force = normalizedDirection * knockbackAmount * Mathf.Max(rigidBody.mass, 0.0001f);

        Debug.Log($"Target Pos: {transform.position} | Source Pos: {source} | Norm: {normalizedDirection} | Force: {force}");

        rigidBody.AddForce(force, ForceMode2D.Impulse);
        knockbackTime = Time.time;
    }
}
