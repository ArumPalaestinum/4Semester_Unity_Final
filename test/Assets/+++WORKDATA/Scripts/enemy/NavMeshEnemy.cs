using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemy : MonoBehaviour
{
    public Transform player; //for player positoon 
    private NavMeshAgent agent; 

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();//run navmesh?
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position; //playerposition
    }
}
