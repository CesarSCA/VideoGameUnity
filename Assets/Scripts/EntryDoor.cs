using UnityEngine;

public class EntryDoor : MonoBehaviour, IClicked
{
        [SerializeField] public bool openned = true;
        [SerializeField] public Animator anim;

 
        private void Start()
        {
            anim = GetComponent<Animator>();
            anim.SetBool("IsOpen", true);

        }
    private void OnEnable()
    {
        GetKey.OnGetGarageKey += OpenDoor;
        LeavePrincipalPath.OnForestEnter += CloseDoor;
    }
    private void OnDisable()
    {
        GetKey.OnGetGarageKey -= OpenDoor;
        LeavePrincipalPath.OnForestEnter -= CloseDoor;
    }

    public void OnClickAction()
        {
                if (openned == true)
                {
                    anim.SetBool("IsOpen", false);
                    openned = false;
                }
        }
    private void CloseDoor()
    {
        anim.SetBool("IsOpen", false);
        openned = false;
    }
    private void OpenDoor()
    {
            anim.SetBool("IsOpen", true);
            openned = true;
    }
}
