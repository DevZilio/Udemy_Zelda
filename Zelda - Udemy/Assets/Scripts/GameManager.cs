using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyState
{
    IDLE,
    ALERT,
    PATROL,
    FOLLOW,
    EXPLORE,
    FURY,
    ATTACK,
    DEAD
}


public class GameManager : MonoBehaviour
{
    [Header("Slime IA")]
    public float slimeIdleWaitTime;
    public Transform[] slimeWayPoints;
    public float slimeDistanceToAttack = 2.25f;
    public float slimeAlertTime = 3f;
    public float slimeAttackDelay = 1f;
    public float slimeLookAtSpeed = 1f;
    public float slimeTimeFollowLimit = 2f;

    [Header("Player")]
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
