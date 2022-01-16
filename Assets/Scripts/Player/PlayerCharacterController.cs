using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{

    [SerializeField] AudioSource walkingOnGrass;
    [SerializeField] AudioSource walkingOnHouseFloor;
    AudioSource currentWalk;
    public CursorMode cursorMode = CursorMode.Auto;
    [SerializeField]private float speed = 4.1f;
    private float Gravity = -9.81f;
    private Vector3 velocity;
    private float ejeX = 0f;
    private CharacterController cc;
    public bool startedWalking = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {

        Cursor.SetCursor(default, Vector2.zero, cursorMode);
        cc = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        FirstCinematic.OnFirstCinematic += ForFirstCinematic;
        SecondCinematic.OnSecondCinematic += ForSecondCinematic;
    }
    private void OnDisable()
    {
        FirstCinematic.OnFirstCinematic -= ForFirstCinematic;
        SecondCinematic.OnSecondCinematic -= ForSecondCinematic;
    }
    void Update()
    {
        if(currentWalk != null && currentWalk.isPlaying)
        {
            if(ControlPlayer.Instance.executing == true || GameManager.Instance.onPause == true)
            {
                currentWalk.Pause();
            }
        }
        if (ControlPlayer.Instance.executing != true && GameManager.Instance.onPause != true)
        {
            Move();
            Rotate();
        }
        velocity.y += Gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.Q))
        {

            if(InventoryManager.Instance.inventory.isFull != true)
            {
                Debug.Log("You do not have any item");
            }
            else
            {
                Instantiate(InventoryManager.Instance.inventory.item.itemObject, this.transform.position - new Vector3(0, 0.96f, 0), this.transform.rotation);
                InventoryManager.Instance.RemoveItem();
            }
        }
    }
    void ForFirstCinematic()
    {
        currentWalk = walkingOnGrass;
    }
    void ForSecondCinematic()
    {
        currentWalk = walkingOnHouseFloor;
    }
    void Rotate()
    {
        ejeX += Input.GetAxis("Mouse X");
        Quaternion angulo = Quaternion.Euler(0, ejeX, 0);
        transform.rotation = angulo;
    }

    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

     
        if (direction.magnitude >= 0.1f)
        {
            cc.Move(direction.normalized * speed * Time.deltaTime);
            if(startedWalking == false)
            {
                startedWalking = true;
                AttentionManager.Instance.IncreaseNoise(30);
                currentWalk.Play();
                
            }
        }
        if (direction.magnitude == 0f)
        {
            if(startedWalking == true)
            {

                startedWalking = false;
                AttentionManager.Instance.DecreaseNoise(30);
                currentWalk.Stop();
            }
        }
    }
}