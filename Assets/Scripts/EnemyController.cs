using System.Collections;
using System.Collections.Generic;
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
            if (enemyAgent.velocity.magnitude > 0)
            {
                if (animEnemy.GetBool("IsRunning") != true)
                {
                    animEnemy.SetBool("IsWalking", true);
                }
            }
            else
            {
                animEnemy.SetBool("IsWalking", false);
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
            case (60):
                enemyAgent.speed = 3.3f;
                break;
            case (80):
                enemyAgent.speed = 3.5f;
                break;
            case (100):
                enemyAgent.speed = 3.8f;
                break;
        }
    }
    void ForFirstCinematic()
    {
        canMove = true;
        enemyAgent.speed = 4f;
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
                    enemyAgent.isStopped = true;
                }
                else
                {
                    enemyAgent.isStopped = false;
                }

            }
            else if (other.GetComponent<EntryDoor>())
            {
                if (other.GetComponent<EntryDoor>().openned == false)
                {
                    enemyAgent.isStopped = true;
                    animEnemy.SetBool("IsRunning", false);
                }
                else
                {
                    enemyAgent.isStopped = false;
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
