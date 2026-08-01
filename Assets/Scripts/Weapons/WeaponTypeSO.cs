using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon Type", order = 1)]
public class WeaponTypeSO : InventoryItemSO
{
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField] public bool AppliesKnockback { get; private set; }
    [field: SerializeField] public float KnockbackAmount { get; private set; }
    [field: SerializeField] public float HeldAngleOffset { get; private set; }
    

    [field: SerializeField] public float Range { get; private set; }
}
