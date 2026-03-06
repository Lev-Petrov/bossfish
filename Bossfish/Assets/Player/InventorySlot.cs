
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemData item;
    public Image icon;

    public void AddItem(ItemData newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void Clean()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}