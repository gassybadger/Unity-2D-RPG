using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Type", menuName = "Weapons/Weapon Type")]
public class WeaponTypeSO : InventoryItemSO
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public float KnockbackAmount { get; private set; }

    [Range(-359f, 359f)]
    [field: SerializeField] public float HeldAngleOffset { get; private set; }
}
