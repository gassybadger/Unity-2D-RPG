using UnityEngine;

public abstract class AbstractWeapon
    : MonoBehaviour
    , IWeapon
{
    [field: SerializeField] public WeaponTypeSO WeaponTypeSO { get; private set; }
    InventoryItemSO IEquipable.InventoryItemSO => WeaponTypeSO;

    public Transform Transform => transform;


    private float _lastAttack;

    protected virtual void Awake()
    {
        _lastAttack = Time.time + WeaponTypeSO.Cooldown;
    }

    public void Use() => Attack();
    public void Attack()
    {
        float attackDelta = Time.time - _lastAttack;
        if (attackDelta > WeaponTypeSO.Cooldown)
        {
            PerformAttack();
            _lastAttack = Time.time;
        }
    }

    protected abstract void PerformAttack();
}