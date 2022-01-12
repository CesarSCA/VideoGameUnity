using System.Collections;
using UnityEngine;

public class GetItem : MonoBehaviour, IClicked
{
    public Item item;
    public void OnClickAction()
    {
        if(InventoryManager.Instance.inventory.isFull)
        {
            Debug.Log("Tu inventario esta lleno");
        }
        else
        {
            InventoryManager.Instance.AddItem(item);
            Destroy(gameObject);
        }
    }
}
