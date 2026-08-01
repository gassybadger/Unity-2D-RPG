using Unity.VisualScripting;
using UnityEngine;

public class UIInventoryContainer : MonoBehaviour
{
    [SerializeField] private UIInventoryButton[] _inventoryButtonArray;

    private int _currentSlotIndex = 0;


    private void Awake()
    {
        EventBus<ActiveInventoryChanged>.OnEvent += OnActiveInventoryChanged;
    }

    private void Start()
    {
        if (_inventoryButtonArray != null && _inventoryButtonArray.Length > 0)
        {
            _inventoryButtonArray[0].Select();
        }
    }

    private void OnActiveInventoryChanged(ActiveInventoryChanged @event)
    {
        int currentSlot = _currentSlotIndex;

        if (@event.IsDelta)
        {
            _currentSlotIndex += @event.SlotIndex;
        }
        else
        {
            _currentSlotIndex = @event.SlotIndex;
        }

        while (true)
        {
            if (_currentSlotIndex < 0)
            {
                _currentSlotIndex = _inventoryButtonArray.Length - 1;
            }
            else if (_currentSlotIndex >= _inventoryButtonArray.Length)
            {
                _currentSlotIndex = 0;
            }

            if (!_inventoryButtonArray[_currentSlotIndex].IsEnabled)
            {
                if (@event.IsDelta)
                {
                    _currentSlotIndex += @event.SlotIndex;
                    continue;
                }

                _currentSlotIndex = currentSlot;
                return;
            }

            foreach (UIInventoryButton inventoryButton in _inventoryButtonArray)
            {
                inventoryButton.Deselect();
            }

            _inventoryButtonArray[_currentSlotIndex].Select();
            break;
        }
    }

    private void ActivateCurrentSlot()
    {
    }
}
