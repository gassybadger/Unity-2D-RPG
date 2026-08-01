using UnityEngine;


public class DamageSource : MonoBehaviour
{
    public WeaponTypeSO WeaponType { get; private set; }

    public void SetWeaponType(WeaponTypeSO weaponTypeSO)
    {
        WeaponType = weaponTypeSO;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (WeaponType == null) 
        {
            Debug.LogWarning($"{gameObject} has no WeaponTypeSO assigned. Assign it from the currently active weapon.");
            return; 
        }

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(WeaponType.Damage);

            // Anything after this line is likely a candidate for refactor. But not sure yet.
            if (damageable.Transform.TryGetComponent(out Knockback knockbackable))
            {
                knockbackable.ApplyKnockbackForce(transform, WeaponType.KnockbackAmount);
            }
        }
    }
}
