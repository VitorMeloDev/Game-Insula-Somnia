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

    public const float idleWaitTime = 3f;
    public const float patrolWaitTime = 10f;

    //IA
    private NavMeshAgent agent;
    private Vector3 destination;
    private int idWayPoint;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        ChangeState(state);
    }

    // Update is called once per frame
    void Update()
    {
        StateManager();
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

            break;

            case enemyState.FURY:

            break;
            
            case enemyState.PATROL:
                print("Patrulhando");
            break;
        }
    }

    void ChangeState(enemyState newState)
    {
        StopAllCoroutines();
        state = newState;

        switch (state)
        {
            case enemyState.IDLE:
                destination = transform.position;
                agent.destination = destination;
                StartCoroutine(IDLE());
            break;

            case enemyState.ALERT:

            break;
            
            case enemyState.EXPLORE:

            break;

            case enemyState.FOLLOW:

            break;

            case enemyState.FURY:

            break;
            
            case enemyState.PATROL:
                idWayPoint = Random.Range(0, _gameManager.slimeWayPoints.Length);
                destination = _gameManager.slimeWayPoints[idWayPoint].position;
                agent.destination = destination;
                StartCoroutine(PATROL());
            break;
        }
    }  

    IEnumerator IDLE()
    {
        yield return new WaitForSeconds(idleWaitTime);
        StayStill(50);
    }   

    IEnumerator PATROL()
    {
        yield return new WaitForSeconds(patrolWaitTime);
        StayStill(30);
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
}
