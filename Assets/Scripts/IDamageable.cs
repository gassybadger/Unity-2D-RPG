
using UnityEngine;

public interface IDamageable
{
    Transform Transform { get; }

    int MaxHealth { get; }
    int CurrentHealth { get; }


    void Damage(DamageContext context);

    void Heal(int amount);
    
    void Die();


    public struct DamageContext
    {
        public Vector3 Source { get; private set; }

        public int Amount { get; private set; }

        public bool ApplyKnockback { get; private set; }
        public float KnockbackForce { get; private set; }

        public DamageContext(int amount)
        {
            Amount = amount;
            ApplyKnockback = false;
            KnockbackForce = 0;
            Source = Vector3.zero;
        }

        public DamageContext(int amount, Vector3 sourcePosition, float knockbackForce)
            : this(amount)
        {
            Source = sourcePosition;
            ApplyKnockback = knockbackForce > 0;
            KnockbackForce = knockbackForce;
        }
    }
}

