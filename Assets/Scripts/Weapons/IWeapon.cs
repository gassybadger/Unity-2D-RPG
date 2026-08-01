public interface IWeapon : IEquipable
{
    WeaponTypeSO WeaponTypeSO { get; }

    void Attack();
}