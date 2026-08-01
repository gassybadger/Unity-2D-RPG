public struct ActiveInventoryChanged : IEvent
{
    public int SlotIndex { get; }
    public bool IsDelta { get; }

    public ActiveInventoryChanged(int slot, bool isDelta = false)
    {
        SlotIndex = slot;
        IsDelta = isDelta;
    }
}
