using UnityEngine;

public class Doors: MonoBehaviour, IClicked
{
    [SerializeField] private Key.KeyType keyType = Key.KeyType.nothing;
    [SerializeField] public bool openned;
    [SerializeField] bool canOpen;
    [SerializeField] Animator anim;
    [SerializeField] GetKey key;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnClickAction()
   {
        if(InventoryManager.Instance.inventory.item != null)
        {
           key = InventoryManager.Instance.inventory.item.itemObject.GetComponent<GetKey>();
        } 

        if (keyType == Key.KeyType.nothing || canOpen == true)
        {
            if(canOpen == true || keyType == Key.KeyType.nothing)
            {
                OpenDoor();
            }
        } else if( key != null)
        {
            if(key.key == keyType)
            {
                OpenDoor();
            }
            else
            {
                HelpTextManager.Instance.ShowText("You don´t have the key to open this door");
            }
        }
        else
        {
            HelpTextManager.Instance.ShowText("You don´t have the key to open this door");
        }
    }
    void OpenDoor()
    {
        canOpen = true;
        if (openned == true)
        {
            anim.SetBool("IsOpen", false);
            openned = false;
        }
        else if (openned == false)
        {
            anim.SetBool("IsOpen", true);
            openned = true;
        }
    }
}
