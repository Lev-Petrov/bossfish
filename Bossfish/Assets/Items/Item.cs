using UnityEngine;

public class Item : Interactive
{
    public  ItemData data;
    public override void Interaction(GameObject interactor)
    {
        Inventory inventory;
        if(interactor.TryGetComponent<Inventory>(out inventory))
        {
            inventory.AddItem(data);
            Destroy(gameObject);
        }
    }
}
