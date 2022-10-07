using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{

    private NavMeshAgent npc;
    public List<Vector3> patrolRoute;
    private int next = 0 ;
    public Transform player;

    void Start()
    {
        npc = GetComponent<NavMeshAgent>();
        npc.destination = patrolRoute[0];
    }

    void Update()
    {
        // let npc patrol through points << HERE IS WHERE THE ANIMATED DUDE IS GETTING STUCK I THINK?
        if(Vector3.Distance(transform.position, patrolRoute[next]) < 0.1f){
            next = (next + 1 == patrolRoute.Count) ? 0 : next + 1;
            //next++;
            npc.destination = patrolRoute[next];
        }
        // distance from player to npc
        Vector3 npcToPlayer = Vector3.Normalize(player.position - transform.position);

        // check if angle is less than 30
        if(Vector3.Angle(transform.forward, npcToPlayer) < 30.0f){
            Ray ray = new Ray(transform.position, npcToPlayer);
            RaycastHit hit;

            // if ray hit
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
                if(hit.collider.tag == "Player"){
                    // set npc to chase
                    npc.destination = hit.collider.transform.position;
                } else {
                    // otherwise continue on route
                    npc.destination = patrolRoute[next];
                }
            }
        // otherwise continue on route
        } else {
            npc.destination = patrolRoute[next];
        }

        // make npc face direction of travel
        if(npc.velocity.sqrMagnitude > Mathf.Epsilon){
            transform.rotation = Quaternion.LookRotation(npc.velocity.normalized);
        }
    }
}