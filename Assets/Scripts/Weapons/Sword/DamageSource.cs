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
            float knockbackAmount = WeaponType.AppliesKnockback ? WeaponType.KnockbackAmount : 0f;

            damageable.Damage(
                new IDamageable.DamageContext(
                    WeaponType.Damage, 
                    transform.position, 
                    knockbackAmount));
        }
    }
}
