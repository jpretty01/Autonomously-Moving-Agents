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

    // Used for Wander function
    Vector3 wanderTarget = Vector3.zero;

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

    //Pursue
    void Pursue()
    {
        // Determing where the target is going
        Vector3 targetDir = target.transform.position - this.transform.position;

        // Angle between forward directions
        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        // If the character stops moving, Seek and stop looking ahead
        if((toTarget > 90 && relativeHeading < 20 || target.GetComponent<Drive>().currentSpeed < 0.01f))
        {
            Seek(target.transform.position);
            return;
        }

        //How far to look ahead
        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    //Evade
    void Evade()
    {
        // Determing where the target is going
        Vector3 targetDir = target.transform.position - this.transform.position;

        //How far to look ahead to run away
        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);

    }

 
    //Wander
    void Wander()
    {
        //Used for helping wander around
        float wanderRadius = 50;
        float wanderDistance = 20;
        float wanderJitter = 10;

        // Moves  wander target
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        // Moves local target out
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        // World distance that can be seen
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);

    }
    // Update is called once per frame
    void Update()
    {
        // Who are we going after?
        //Seek(target.transform.position);

        //Flee(target.transform.position);

        //Pursue();
        //Evade();

        //Wander();
    }
}
