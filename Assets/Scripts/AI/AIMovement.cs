using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{

    public NavMeshAgent npc;
    public List<Vector3> patrolRoute;
    public Vector3 LastSeen;
    private int next = 0 ;
    public Transform player;

    private BaseStateMachine<AIMovement> mStateMachine;


    public void SetSpeed(float speed){
        npc.speed = speed;
    }
    // get position
    public Vector3 Position {
        get {
            return transform.position;
        }
    }
    // get player's position
    public Vector3 PlayerPosition {
        get {
            return player.position;
        }
    }

    // get next patrol point
    public Vector3 PatrolPoint {
        get {
            return patrolRoute[next];
        }
    }

    void Start()
    {
       // npc = GetComponent<NavMeshAgent>();
        //npc.destination = patrolRoute[0];
        mStateMachine = new BaseStateMachine<AIMovement>(this, new PatrolState());
    }

    public void ChangeState(BaseState<AIMovement> state) {
        // change state machine state
        mStateMachine.ChangeState(state);
        
    }

    // go to next patrol point
    public Vector3 MoveNext(){
        next = (next + 1 == patrolRoute.Count) ? 0 : next + 1;
        return patrolRoute[next];    
    }

    // move to specific position
    public void MoveTo(Vector3 pos){
        npc.destination = pos;
    }

    // check if player is in view
    public bool InView(float viewAngle){
        Vector3 npcToPlayer = Vector3.Normalize(player.position - transform.position);

        // check if angle is less than 30
        if(Vector3.Angle(transform.forward, npcToPlayer) < viewAngle){
            Ray ray = new Ray(transform.position, npcToPlayer);
            RaycastHit hit;

            // if ray hit
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
                if(hit.collider.tag == "Player"){
                    // set npc to chase
                    LastSeen = hit.point;
                    return true;
                }
            }
        // otherwise continue on route
        } 
        return false;
    }

    void Update()
    {
        // update state machine
        mStateMachine.Update();

        // make npc face direction of travel
        if(npc.velocity.sqrMagnitude > Mathf.Epsilon){
            transform.rotation = Quaternion.LookRotation(npc.velocity.normalized);
        }


        // let npc patrol through points << HERE IS WHERE THE ANIMATED DUDE IS GETTING STUCK I THINK?
        // if(Vector3.Distance(transform.position, patrolRoute[next]) < 0.1f){
        //     MoveNext();
        // }
        // // distance from player to npc
        // Vector3 npcToPlayer = Vector3.Normalize(player.position - transform.position);

        // // check if angle is less than 30
        // if(Vector3.Angle(transform.forward, npcToPlayer) < 30.0f){
        //     Ray ray = new Ray(transform.position, npcToPlayer);
        //     RaycastHit hit;

        //     // if ray hit
        //     if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
        //         if(hit.collider.tag == "Player"){
        //             // set npc to chase
        //             npc.destination = hit.collider.transform.position;
        //         } else {
        //             // otherwise continue on route
        //             npc.destination = patrolRoute[next];
        //         }
        //     }
        // // otherwise continue on route
        // } else {
        //     npc.destination = patrolRoute[next];
        // }

        // // make npc face direction of travel
        // if(npc.velocity.sqrMagnitude > Mathf.Epsilon){
        //     transform.rotation = Quaternion.LookRotation(npc.velocity.normalized);
        // }
    }
}