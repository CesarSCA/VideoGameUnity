using UnityEngine;
using System;

public class GetKey: MonoBehaviour, IClicked
{
    public Item item;
    public Key.KeyType key;
    public static event Action OnGetGarageKey;

    public void OnClickAction()
    {
        if(InventoryManager.Instance.inventory.isFull == true)
        {
            HelpTextManager.Instance.ShowText("Your inventory is full!");
        }
        else
        {
            if (key == Key.KeyType.garage && TimeController.Instance.day >= 1)
            {
                OnGetGarageKey?.Invoke();
            }
            InventoryManager.Instance.AddItem(item);
            Destroy(gameObject);
        }


    }
}
