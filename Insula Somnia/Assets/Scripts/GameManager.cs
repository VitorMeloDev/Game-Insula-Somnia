using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyState
{
    IDLE, ALERT, EXPLORE, PATROL, FOLLOW, FURY
}

public class GameManager : MonoBehaviour
{
    public Transform player;

    [Header("Slime IA")]
    public float slimeIdleWaitTime = 3f;
    public float slimeDistanceToAttack;
    public float slimeAlertTime = 3f;
    public float slimeAttackDelay = 3f;
    public Transform[] slimeWayPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
