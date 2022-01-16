using UnityEngine;

public class InventoryManager: MonoBehaviour
{
    protected InventoryManager() { }
    public static InventoryManager Instance;
    public Inventory inventory;

    private void Awake()
    {
        Instance = this;
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        inventory.slot.enabled = false;
    }
    public void AddItem(Item item)
    {
        inventory.item = item;
        inventory.isFull = true;
        inventory.slot.sprite = item.sprite;
        inventory.slot.enabled = true;
    }
    public void RemoveItem()
    {
        inventory.item = null;
        inventory.isFull = false;
        inventory.slot.sprite = null;
        inventory.slot.enabled = false;
    }
}
