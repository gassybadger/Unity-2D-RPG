using UnityEngine;

public class Sword : AbstractWeapon
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DamageSource _damageSource;

    protected override void Awake()
    {
        base.Awake();

        _damageSource.SetWeaponType(WeaponTypeSO);
    }

    protected override void PerformAttack()
    {
        _animator.SetTrigger("Attack");
    }
}
