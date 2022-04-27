using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float minDistanceToTarget = 5;
    [SerializeField] float maxDistanceToTarget = 25;
    [SerializeField] float maxDistanceToTargetWithoutFlashlight = 5;
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float wanderSpeed = 1;

    NavMeshAgent navMeshAgent;
    PlayerMovement playerMovement;

    Vector3 target;
    bool isChasingPlayer = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        navMeshAgent.speed = wanderSpeed;
    }

    private void Update()
    {
        // If Enemy Is Close To Player
        // And the player is using a flashlight
        float distance = Vector3.Distance(this.transform.position, playerMovement.gameObject.transform.position);
        if (distance > minDistanceToTarget && distance < maxDistanceToTarget
            && playerMovement.CanBeSeenByEnemies()
            || distance < maxDistanceToTargetWithoutFlashlight)
        {
            // Set the movementspeed, and destination of the navMeshAgent
            navMeshAgent.speed = movementSpeed;
            isChasingPlayer = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(playerMovement.gameObject.transform.position);
        }
        else
        {
            // Else if he was chasing the player, calculate a new target pos,
            // and set the movement speed to the wander speed
            if (isChasingPlayer)
            {
                CalculateNewTargetPos();
                navMeshAgent.isStopped = false;
                navMeshAgent.speed = wanderSpeed;
                isChasingPlayer = false;
            }
            else
            {
                // If close to target, 
                // Stop for a few seconds, and recalculate target
                if (Vector3.Distance(this.transform.position, target) < minDistanceToTarget)
                {
                    navMeshAgent.isStopped = true;
                    StartCoroutine(WaitAtTarget());
                }
            }

        }

    }

    void CalculateNewTargetPos()
    {
        float maxDistance = 30.0f;

        Vector3 posDiff = new Vector3(Random.Range(-maxDistance, maxDistance), 0, Random.Range(-maxDistance, maxDistance));

        target = posDiff + this.transform.position;

        navMeshAgent.SetDestination(target);
    }

    IEnumerator WaitAtTarget()
    {
        yield return new WaitForSeconds(2.0f);
        navMeshAgent.isStopped = false;
        CalculateNewTargetPos();
    }
}
