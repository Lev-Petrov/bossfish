
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public InputAction switchAction;
    public InputAction dropAction;
    public InventorySlot[] slots;
    public RectTransform frame;
    public Transform dropPoint;
    public float dropForce;
    int activeSlotIndex;

    private void OnEnable()
    {
        switchAction.Enable();
        dropAction.Enable();
        switchAction.performed += OnSwitch;
        dropAction.performed += OnDrop;
    }
    private void OnDisable()
    {
        switchAction.Disable();
        dropAction.Disable();
        switchAction.performed -= OnSwitch;
        dropAction.performed -= OnDrop;
    }

    private void OnSwitch(InputAction.CallbackContext context)
    {
        float scroll = context.ReadValue<float>();
        if (scroll == 0) return;

        // Конвертуємо у int (-1 або +1)
        int delta = scroll > 0 ? 1 : -1;

        activeSlotIndex = (activeSlotIndex + delta + slots.Length) % slots.Length;

        // Рух рамки до активного слоту (UI Canvas)
        frame.anchoredPosition = slots[activeSlotIndex].GetComponent<RectTransform>().anchoredPosition;
    }

    private void OnDrop(InputAction.CallbackContext context)
    {
        GameObject dropedItem = Instantiate(slots[activeSlotIndex].item.materialObject, dropPoint.position, dropPoint.rotation);
        slots[activeSlotIndex].Clean();

        Rigidbody itemRb;
        if (dropedItem.TryGetComponent<Rigidbody>(out itemRb))
        {
            itemRb.AddForce(dropPoint.forward * dropForce, ForceMode.Impulse);
        }
    }

    public void AddItem(ItemData item)
    {
        for (int i = 0; i < slots.Length; i++)
        { 
            if (slots[i].item == null)
            {
                slots[i].AddItem(item);
                return;
            }
        }

        Instantiate(item.materialObject);
    }
}
