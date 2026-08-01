using UnityEngine;

public class DestructableObject : AbstractDamageable
{
    [SerializeField] private ParticleSystem _deathVfx;
    [SerializeField] private int _maxHealth = 1;

    protected override void Awake()
    {
        base.Awake();

        CurrentHealth = MaxHealth = _maxHealth;
    }

    protected override ParticleSystem GetDeathVFX() => _deathVfx;
}