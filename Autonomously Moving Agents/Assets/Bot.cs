using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    // Creating the new NavMeshAgent
    NavMeshAgent agent;

    // Game object of the target
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the navmesh for this agent
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Seeking out whoever we need too
    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    // Flee!
    void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    // Update is called once per frame
    void Update()
    {
        // Who are we going after?
        //Seek(target.transform.position);

        Flee(target.transform.position);
    }
}
