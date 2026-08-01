public struct InventorySlotSelection : IEvent
{
    public int SlotIndex { get; }
    public bool IsDelta { get; }

    public InventorySlotSelection(int slot, bool isDelta = false)
    {
        SlotIndex = slot;
        IsDelta = isDelta;
    }
}