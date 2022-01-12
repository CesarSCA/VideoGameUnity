using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager: MonoBehaviour
{
    // Start is called before the first frame update
    protected InventoryManager() { }

    public static InventoryManager Instance;
 

    public Inventory inventory;

    private void Awake()
    {

        Instance = this;
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        inventory.slot.enabled = false;
    }
    private void Update()
    {
        if(inventory.item != null)
        {
            if (inventory.item.itemObject.GetComponent<GetItem>())
            {
                Debug.Log("anda");
            }
        }

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
