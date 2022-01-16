using UnityEngine;

public class OpenFurtniture : MonoBehaviour, IClicked
{
    // Start is called before the first frame update
    [SerializeField] bool IsOpen;
    [SerializeField] Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
    void InteractueFurniture()
    {
        Debug.Log("AA");
        if (IsOpen == true)
        {
            IsOpen = false;
            anim.SetBool("IsOpen", false);
        }
        else
        {
            IsOpen = true;
            anim.SetBool("IsOpen", true);
        }
    }

    public void OnClickAction()
    {

        InteractueFurniture();
    }

}
