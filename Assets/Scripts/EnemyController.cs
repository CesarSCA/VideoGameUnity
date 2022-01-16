using System.Collections;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Animator animEnemy;
    private NavMeshAgent enemyAgent;
    public static event Action OnPlayerDeath;
    [SerializeField] Transform deathZone;
    public bool canMove = true;
    [SerializeField] Transform lookAt;
    [SerializeField] GameObject canvas;
    bool isInForest = false;

    void Awake()
    {
        animEnemy = GetComponent<Animator>();
        enemyAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");       

    }
    private void OnEnable()
    {
        FirstCinematic.OnFirstCinematic += ForFirstCinematic;
        LeavePrincipalPath.OnForestEnter += PlayerEnteredForest;
    }
    private void OnDisable()
    {
        FirstCinematic.OnFirstCinematic -= ForFirstCinematic;
        LeavePrincipalPath.OnForestEnter -= PlayerEnteredForest;
    }

    // Update is called once per frame
    void Update()
    {
        if(ControlPlayer.Instance.executing != true && canMove)
        {
            enemyAgent.destination = player.transform.position;
            switch (enemyAgent.speed)
            {
                case(0):
                    animEnemy.SetBool("IsRunning", false);
                    animEnemy.SetBool("IsWalking", false);
                    break;
                case var _ when enemyAgent.speed < 0.1f && enemyAgent.speed > 3.9:
                    animEnemy.SetBool("IsRunning", false);
                    animEnemy.SetBool("IsWalking", true);
                    break;
                case var _ when enemyAgent.speed >= 4f:
                    animEnemy.SetBool("IsWalking", false);
                    animEnemy.SetBool("IsRunning", true);
                    break;
            }

            if (isInForest)
            {
                MoveByNoise();
            }

        }
    }
    void MoveByNoise()
    {
        float attention = AttentionManager.Instance.playerAttention;
        switch (attention)
        {
            case (0):
                enemyAgent.speed = 1f;
                break;
            case var _ when attention < 30 && attention > 0:
                enemyAgent.speed = 3f;
                break;
            case var _ when attention >= 30 && attention < 40:
                enemyAgent.speed = 3.7f;
                break;
        }
    }
    void ForFirstCinematic()
    {
        canMove = true;
        enemyAgent.speed = 4.05f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            StartCoroutine(KillingPlayer(other.transform));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (other.GetComponent<Doors>())
            {
                if (other.GetComponent<Doors>().openned == false)
                {
                    enemyAgent.speed = 0;
                }
                else
                {
                    enemyAgent.speed = 4.05f;
                }

            }
            else if (other.GetComponent<EntryDoor>())
            {
                if (other.GetComponent<EntryDoor>().openned == false)
                {
                    enemyAgent.speed = 0;
                }
                else
                {
                    enemyAgent.speed = 4.05f;
                }
            }
        }
    }
    IEnumerator KillingPlayer(Transform player)
    {
        animEnemy.SetBool("IsRunning", true);
        canvas.SetActive(false);
        ControlPlayer.Instance.executing = true;
        canMove = false;
        StartCoroutine(PlayerMoving(player));
        yield return new WaitForSeconds(5f);
        OnPlayerDeath?.Invoke();
    }
    IEnumerator PlayerMoving(Transform player)
    {
        while (true)
        {
            ControlPlayer.Instance.LookAt(lookAt);
            player.position = deathZone.position;
            yield return null;
        }
    }
    void PlayerEnteredForest()
    {
        isInForest = true;
        enemyAgent.speed = 3.2f;
    }
}
