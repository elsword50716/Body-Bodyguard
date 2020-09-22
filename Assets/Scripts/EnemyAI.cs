using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform Ship;

    public float currentHealth;

    public EnemyData enemyData = new EnemyData();


    private enum State
    {
        Roaming,
        ChaseTarget,
    }
    private Vector3 startingPosotion;
    private Vector3 roamPosotion;
    private bool isReachTarget = false;
    private State state;


    private void Awake()
    {
        Ship = GameObject.FindGameObjectWithTag("Ship").transform;
        state = State.Roaming;
    }

    private void Start()
    {
        currentHealth = enemyData.maxHealth;
        startingPosotion = transform.position;
        roamPosotion = GetRoamingPostion();
    }

    private void Update()
    {
        FindTarget();
        
        switch (state)
        {

            default:

            case State.Roaming:
                MoveTo(roamPosotion);
                Debug.DrawLine(transform.position, roamPosotion, Color.green);
                float reachedPositionDistance = 1f;
                if (Vector3.Distance(transform.position, roamPosotion) < reachedPositionDistance)
                {
                    roamPosotion = GetRoamingPostion();
                }

                

                break;

            case State.ChaseTarget:
                MoveTo(Ship.position * enemyData.ClosestRangeToShip);
                Debug.DrawLine(transform.position, transform.position + (Ship.position * enemyData.ClosestRangeToShip), Color.red);

                break;
        }

    }

    private Vector3 GetRoamingPostion()
    {
        return startingPosotion + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * enemyData.roamRange;
    }

    private void MoveTo(Vector3 targetPosition)
    {
        transform.Translate((targetPosition - transform.position).normalized * enemyData.moveSpeed * Time.deltaTime);
    }

    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, Ship.position) < enemyData.detectShipRange)
        {
            state = State.ChaseTarget;
            Vector3.Lerp(transform.position, Ship.position, enemyData.ClosestRangeToShip);
        }
        else
        {
            state = State.Roaming;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.detectShipRange);
        

    }
}
