using UnityEngine;

public class Bow
    : MonoBehaviour
    , IWeapon
{
    [field:SerializeField] public WeaponTypeSO WeaponTypeSO { get; private set; }
    InventoryItemSO IEquipable.InventoryItemSO => WeaponTypeSO;

    public Transform Transform => throw new System.NotImplementedException();


    public void Use() => Attack();
    public void Attack()
    {

    }
}
