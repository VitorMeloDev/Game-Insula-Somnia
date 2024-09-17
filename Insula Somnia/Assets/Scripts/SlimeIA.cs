using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class SlimeIA : MonoBehaviour
{
    private GameManager _gameManager;

    private Animator anim;
    private bool isDie = false;
    public int HP = 3;
    public enemyState state;

    //IA
    private bool isWalk;
    private bool isAlert;
    private bool isPlayerVisible;
    private bool isAttack;

    private NavMeshAgent agent;
    private Vector3 destination;
    private int idWayPoint;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        //ChangeState(state);
    }

    // Update is called once per frame
    void Update()
    {
        StateManager();

        if(agent.desiredVelocity.magnitude >= 0.1f)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }

        anim.SetBool("isWalk", isWalk);
        anim.SetBool("isAlert", isAlert);
    }

    IEnumerator Die()
    {
        isDie = true;
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    void GetHit(int amount)
    {
        if(isDie) {return;}
        HP -= amount;
        
        if(HP > 0)
        {
            ChangeState(enemyState.FURY);
            anim.SetTrigger("GetHit");
        }
        else
        {
            anim.SetTrigger("Die");
            StartCoroutine(Die());
        }
    }

    void StateManager()
    {
        switch (state)
        {
            case enemyState.IDLE:
                print("Parado");
            break;

            case enemyState.ALERT:
            
            break;
            
            case enemyState.EXPLORE:

            break;

            case enemyState.FOLLOW:
                destination = _gameManager.player.position;
                agent.destination = destination;
            break;

            case enemyState.FURY:
                destination = _gameManager.player.position;
                agent.destination = destination;
            break;
            
            case enemyState.PATROL:
                print("Patrulhando");
            break;
        }
    }

    void ChangeState(enemyState newState)
    {
        StopAllCoroutines();
        isAlert = false;

        switch (newState)
        {
            case enemyState.IDLE:
                destination = transform.position;
                agent.destination = destination;
                agent.stoppingDistance = 0;
                StartCoroutine(IDLE());
            break;

            case enemyState.ALERT:
                destination = transform.position;
                agent.destination = destination;
                agent.stoppingDistance = 0;
                isAlert = true;
                isPlayerVisible = true;
                StartCoroutine(ALERT());
            break;
            
            case enemyState.EXPLORE:

            break;

            case enemyState.FOLLOW:
                agent.stoppingDistance = _gameManager.slimeDistanceToAttack;
                StartCoroutine(FOLLOW());
            break;

            case enemyState.FURY:
                destination = _gameManager.player.position;
                agent.destination = destination;
                agent.stoppingDistance = _gameManager.slimeDistanceToAttack;
                print("Destination");
            break;
            
            case enemyState.PATROL:
                idWayPoint = Random.Range(0, _gameManager.slimeWayPoints.Length);
                destination = _gameManager.slimeWayPoints[idWayPoint].position;
                agent.destination = destination;
                agent.stoppingDistance = 0;
                StartCoroutine(PATROL());
            break;
        }

        state = newState;
    }  

    IEnumerator IDLE()
    {
        yield return new WaitForSeconds(_gameManager.slimeIdleWaitTime);
        StayStill(50);
    }   

    IEnumerator PATROL()
    {
        yield return new WaitUntil(() => agent.remainingDistance <= 0);
        StayStill(30);
    }

    IEnumerator ALERT()
    {
        yield return new WaitForSeconds(_gameManager.slimeAlertTime);

        if(isPlayerVisible)
        {
            ChangeState(enemyState.FOLLOW);
        }
        else
        {
            StayStill(10);
        }
    }

    IEnumerator FOLLOW()
    {
        yield return new WaitUntil(() => !isPlayerVisible);
        
        yield return new WaitForSeconds(_gameManager.slimeAlertTime);

        StayStill(50);
    }

    IEnumerator ATTACK()
    {
        yield return new WaitForSeconds(_gameManager.slimeAttackDelay);
        isAttack = false;
    }   
    void StayStill(int percent)
    {
        if(Rand() < percent)
        {
            ChangeState(enemyState.IDLE);
        }
        else
        {
            ChangeState(enemyState.PATROL);
        }
    }

    int Rand()
    { 
        int rand = Random.Range(0, 100);
        return rand;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(state == enemyState.IDLE || state == enemyState.PATROL)
            {
                ChangeState(enemyState.ALERT);
            }
        }    
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerVisible = false;
        }    
    }
}
