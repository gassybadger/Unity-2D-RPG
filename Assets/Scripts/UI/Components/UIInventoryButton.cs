using UnityEngine;
using UnityEngine.UI;

public class UIInventoryButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InventoryItemSO _inventoryItemSO;
    [SerializeField] private Button _button;
    [SerializeField] private Image _activeHighlight;
    [SerializeField] private Image _itemImage;

    [Header("Settings")]
    [SerializeField] private int _slotIndex;

    

    private void Awake()
    {
        if (_button == null)
        {
            Debug.LogError("UIInventoryButton is not properly configured. Slot disabled");
            gameObject.SetActive(false);
            return;
        }

        SetupButton();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void SetupButton()
    {
        _button.onClick.RemoveAllListeners();
        if (_inventoryItemSO == null)
        {
            _button.interactable = false;
            return;
        }

        // Button
        _button.onClick.AddListener(HandleButtonClick);
        _button.interactable = true;

        // Image
        _itemImage.gameObject.SetActive(true);
        _itemImage.sprite = _inventoryItemSO.Icon;
    }

    private void HandleButtonClick()
    {
        EventBus<ActiveInventoryChanged>.Raise(new ActiveInventoryChanged(_slotIndex));
    }


    public bool IsEnabled => _button.interactable;


    public void Select()
    {
        _activeHighlight.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _activeHighlight.gameObject.SetActive(false);
    }
}
