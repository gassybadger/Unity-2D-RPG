public struct InventorySlotChanged : IEvent
{
    public InventoryItemSO SelectedInventoryItem { get; }

    public InventorySlotChanged(InventoryItemSO selectedInventoryItem)
    {
        SelectedInventoryItem = selectedInventoryItem;
    }
}