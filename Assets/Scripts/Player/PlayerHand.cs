using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _equippedItem;


    private void OnValidate()
    {
        if (_equippedItem != null
            && !_equippedItem.TryGetComponent(out IEquipable _))
        {
            _equippedItem = null;
        }
    }

    private void Awake()
    {
        EventBus<InventorySlotChanged>.OnEvent += HandleInventorySlotChanged;
    }

    private void OnDestroy()
    {
        EventBus<InventorySlotChanged>.OnEvent -= HandleInventorySlotChanged;
    }

    private void Update()
    {
        RotateTowardsMouse();
    }



    public IEquipable EquippedItem { get; private set; }

    private void SetEquippedItem(InventoryItemSO inventoryItemSO)
    {
        if (_equippedItem != null)
        {
            EquippedItem = null;
            Destroy(_equippedItem);
        }

        if (inventoryItemSO == null) 
        {
            return; // Clear hands.
        }

        GameObject selectedGameObject = Instantiate(inventoryItemSO.Prefab, transform.position, transform.rotation, transform);
        if (!selectedGameObject.TryGetComponent(out IEquipable equippable))
        {
            Debug.LogError($"Attempted to equip a non-equipable item - {inventoryItemSO.Name}");
            Destroy(selectedGameObject);
            return;
        }

        _equippedItem = selectedGameObject;
        EquippedItem = equippable;
    }

    private void HandleInventorySlotChanged(InventorySlotChanged @event) => SetEquippedItem(@event.SelectedInventoryItem);


    private void RotateTowardsMouse()
    {
        Vector3 mousePos = PlayerController.Instance.Input.Player.MouseLook.ReadValue<Vector2>();
        Vector3 playerPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        Vector2 dir = mousePos - playerPos;

        // Raw angle from -180 to 180
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float yRotation = 0f;
        float zRotation = angle;

        // If mouse is on the left
        if (Mathf.Abs(angle) > 90)
        {
            yRotation = 180f;
            // Mirror the Z rotation for the 180-degree Y flip
            zRotation = (180f - Mathf.Abs(angle)) * Mathf.Sign(angle);
        }

        float offset = 0f;
        if (EquippedItem is IWeapon weapon)
        {
            offset = weapon.WeaponTypeSO.HeldAngleOffset;
        }

        transform.rotation = Quaternion.Euler(0, yRotation, zRotation + offset);
    }
}