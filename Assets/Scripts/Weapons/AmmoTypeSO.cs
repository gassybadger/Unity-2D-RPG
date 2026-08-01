using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Weapons/Ammo Type", order = 2)]
public class AmmoTypeSO : ScriptableObject
{
    [field: SerializeField] public int Speed { get; private set; }

    [field: SerializeField] public int RangeModifier { get; private set; } = 1;
    [field: SerializeField] public float DamageModifier { get; private set; } = 1;
}
