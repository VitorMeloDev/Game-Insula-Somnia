using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class SlimeIA : MonoBehaviour
{
    private Animator anim;
    private bool isDie = false;
    public int HP = 3;
    public enemyState state;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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

            break;
        }
    }

    void ChangeState(enemyState newState)
    {
        switch (state)
        {
            case enemyState.IDLE:

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

            break;
        }
    }  
}
