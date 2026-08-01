using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Type", menuName = "Weapons/Weapon Type")]
public class WeaponTypeSO : InventoryItemSO
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float AttackDelay { get; private set; }
    [field: SerializeField] public float KnockbackAmount { get; private set; }
}
