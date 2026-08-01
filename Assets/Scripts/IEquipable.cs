using UnityEngine;

public interface IEquipable
{
    Transform Transform { get; }

    InventoryItemSO InventoryItemSO { get; }


    void Use();
}