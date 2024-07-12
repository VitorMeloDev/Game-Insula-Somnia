using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SlimeIA : MonoBehaviour
{
    private Animator anim;
    private bool isDie = false;
    public int HP = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
