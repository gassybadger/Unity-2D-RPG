using UnityEngine;

public class Bow : AbstractWeapon
{
    private static readonly int ANIMATION_TRIGGER = Animator.StringToHash("Fire");

    [field: SerializeField] public Projectile Ammo { get; private set; }

    [SerializeField] private Transform _arrowSpawnLocation;
    private Animator _animator;


    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }


    protected override void PerformAttack()
    {
        _animator.SetTrigger(ANIMATION_TRIGGER);

        Instantiate(Ammo, _arrowSpawnLocation.position, _arrowSpawnLocation.rotation, null).Fire(WeaponTypeSO);
    }
}
