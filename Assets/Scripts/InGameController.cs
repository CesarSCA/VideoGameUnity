using System.Collections;
using UnityEngine;

public class InGameController : MonoBehaviour
{
    // Start is called before the first frame update
    protected InGameController() { }
    public static InGameController Instance;
    [SerializeField] public GameObject UIForGame;
    [SerializeField] GameObject UIInHouse;
    [SerializeField] GameObject UIInForest;
    [SerializeField] GameObject UICinematicText;
    [SerializeField] GameObject enParticles;
    [SerializeField] GameObject onPauseMenu;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        FirstCinematic.OnFirstCinematic += FirstCinematicMethod;
        SecondCinematic.OnSecondCinematic += SecondCinematicMethod;
        FirstFinalCinematic.OnFirstFinal += FirstFinal;
        EnemyController.OnPlayerDeath += EndGame;
        LeavePrincipalPath.OnForestEnter += LeaveForestMethod;
    }
    private void OnDisable()
    {
        FirstCinematic.OnFirstCinematic -= FirstCinematicMethod;
        SecondCinematic.OnSecondCinematic -= SecondCinematicMethod;
        FirstFinalCinematic.OnFirstFinal -= FirstFinal;
        EnemyController.OnPlayerDeath -= EndGame;
        LeavePrincipalPath.OnForestEnter -= LeaveForestMethod;
    }
    void Start()
    {
        onPauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.onPause == false)
            {
                GameManager.Instance.onPause = true;
            }
            else if (GameManager.Instance.onPause == true)
            {
                GameManager.Instance.onPause = false;

            }
            OnPause(GameManager.Instance.onPause);
        }
    }
    public void OnPause(bool pause)
    {
        if (pause == true)
        {
            onPauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (pause == false)
        {
            onPauseMenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void FirstCinematicMethod()
    {
        UIInHouse.SetActive(false);
        StartCoroutine(BlinkEffect());
        FirstCinematic.OnFirstCinematic -= FirstCinematicMethod;
    }
    public void SecondCinematicMethod()
    {
        enParticles.SetActive(false);
        UIInHouse.SetActive(true);
        SecondCinematic.OnSecondCinematic -= SecondCinematicMethod;
    }
    public void LeaveForestMethod()
    {
        UIInForest.SetActive(true);
    }
    public void FirstFinal()
    {
        GameManager.Instance.finals[0] = true;
        GameMenu.Instance.ChangeScene("FirstFinalScene");
    }
    public void EndGame()
    {
        GameMenu.Instance.ChangeScene("GameOver");
    }
    IEnumerator BlinkEffect()
    {
        float time = 15;
        while (time > 0)
        {
            UICinematicText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            UICinematicText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            time -= 0.5f;
        }
        UICinematicText.SetActive(false);
    }
}
