using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Sword 
    : MonoBehaviour
    , IWeapon
{
    [Header("References")]
    [field: SerializeField] public WeaponTypeSO WeaponTypeSO { get; private set; }
    InventoryItemSO IEquipable.InventoryItemSO => WeaponTypeSO;
    private float lastAttack;


    [SerializeField] private Animator _animator;
    [SerializeField] private DamageSource _damageSource;


    public Transform Transform => transform;


    private void Start()
    {
        _damageSource.SetWeaponType(WeaponTypeSO);
        lastAttack = Time.time;
    }


    void IEquipable.Use() => Attack();
    public void Attack()
    {
        float attackDelta = Time.time - lastAttack;
        if (attackDelta >= WeaponTypeSO.Cooldown)
        {
            _animator.SetTrigger("Attack");
            lastAttack = Time.time;
        }
    }
}
