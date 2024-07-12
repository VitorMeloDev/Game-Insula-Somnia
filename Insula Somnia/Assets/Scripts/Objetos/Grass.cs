using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public ParticleSystem fxHit;
    private bool isCut = false;

    void GetHit(int amount)
    {
        if(isCut == false)
        {
            transform.localScale = new Vector3(2f, 2f, 2f);
            fxHit.Emit(10);
            isCut = true;
        }
    }
}
