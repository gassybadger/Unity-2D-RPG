using UnityEngine;

public abstract class AbstractDamageable
    : MonoBehaviour
    , IDamageable
{
    public Transform Transform => transform;

    private Flash damageFlasher;



    protected virtual void Awake()
    {
        TryGetComponent(out damageFlasher);
    }


    protected virtual ParticleSystem GetDeathVFX() => null;

    protected virtual void OnDeath()
    {
        ParticleSystem deathVfx = GetDeathVFX();
        if (deathVfx != null)
        {
            Instantiate(deathVfx, transform.position, Quaternion.identity, null);
        }

        Destroy(gameObject);
    }


    //
    // IDamageable
    //
    public int MaxHealth { get; protected set; }
    public int CurrentHealth { get; protected set; }


    public void Damage(IDamageable.DamageContext context)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - context.Amount, 0, CurrentHealth);
        if (CurrentHealth <= 0)
        {
            Die();
            return;
        }

        if (context.ApplyKnockback && TryGetComponent(out Knockback knockbackable))
        {
            knockbackable.ApplyKnockbackForce(context.Source, context.KnockbackForce);
        }

        if (damageFlasher != null)
        {
            damageFlasher.DamageFlash();
        }

    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, CurrentHealth, MaxHealth);
    }

    public void Die()
    {
        if (damageFlasher != null)
        {
            damageFlasher.DeathFlash(OnDeath);
        }
        else
        {
            OnDeath();
        }
    }


}
