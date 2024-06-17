using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyManager : MonoBehaviour
{
    //FOV parameter
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    //differenciate between target and obstruction
    public LayerMask targetMask;
    public LayerMask obstuctionMask;

    public bool canSeePlayer;

    //Enemy NavMesh
    public Transform player; //for player positoon 
    private NavMeshAgent agent;
    public float spinSpeed = 30f; //spinning speed

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();//run navmesh?
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if (canSeePlayer)
        {
            agent.destination = player.position; //follow player
        }
        else
        {
            SpinAround(); //run spin, if not in view
        }
    }

    private void SpinAround()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstuctionMask))
                {
                    canSeePlayer = true;
                    player = target; 
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else if (canSeePlayer)
            {
                canSeePlayer = false;
            }
        }
    }
}
