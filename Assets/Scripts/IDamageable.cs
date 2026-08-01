
using UnityEngine;

public interface IDamageable
{
    Transform Transform { get; }

    int MaxHealth { get; }
    int CurrentHealth { get; }


    void Damage(int amount);
    void Heal(int amount);
    void Die();
}
