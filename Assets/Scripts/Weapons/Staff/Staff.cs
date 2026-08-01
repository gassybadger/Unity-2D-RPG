using UnityEngine;

public class Staff : AbstractWeapon 
{
    protected override void PerformAttack()
    {
        Debug.Log($"Attacked with the {WeaponTypeSO.Name}");
    }
}