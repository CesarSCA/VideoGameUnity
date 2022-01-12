using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetGarageKey : MonoBehaviour, IClicked
{
    public Item item;
    public Key.KeyType key;


    public void OnClickAction()
    {
        if (InventoryManager.Instance.inventory.isFull == true)
        {
            HelpTextManager.Instance.ShowText("Your inventory is full!");
        }
        else
        {
            InventoryManager.Instance.AddItem(item);
            Destroy(gameObject);
        }


    }
}
