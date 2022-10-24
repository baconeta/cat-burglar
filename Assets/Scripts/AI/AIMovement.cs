using System.Collections.Generic;
using AI.States;
using Controllers;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIMovement : MonoBehaviour
    {
        public NavMeshAgent npc;
        public List<Vector3> patrolRoute;
        public Vector3 lastSeen;
        private int _next;
        public Transform player;
        private ControllerManager _cm;

        public bool meow;
        public bool endGame;
        public Vector3 meowPosition;

        public AudioSource uhoh;

        private BaseStateMachine<AIMovement> _mStateMachine;
        public float rotationSpeed = 2;


        public void SetSpeed(float speed)
        {
            npc.speed = speed;
        }

        // get position
        public Vector3 Position
        {
            get { return transform.position; }
        }

        // get player's position
        public Vector3 PlayerPosition
        {
            get { return player.position; }
        }

        // get next patrol point
        public Vector3 PatrolPoint
        {
            get { return patrolRoute[_next]; }
        }

        private void Start()
        {
            // npc = GetComponent<NavMeshAgent>();
            //npc.destination = patrolRoute[0];
            _mStateMachine = new BaseStateMachine<AIMovement>(this, new PatrolState());
            _cm = FindObjectOfType<ControllerManager>();
            endGame = false;
        }

        public void ChangeState(BaseState<AIMovement> state)
        {
            // change state machine state
            _mStateMachine.ChangeState(state);
        }

        // go to next patrol point
        public Vector3 MoveNext()
        {
            _next = (_next + 1 == patrolRoute.Count) ? 0 : _next + 1;
            return patrolRoute[_next];
        }

        // move to specific position
        public void MoveTo(Vector3 pos)
        {
            npc.destination = pos;
        }

        // check if player is in view
        public bool InView(float viewAngle)
        {
            Vector3 npcToPlayer = Vector3.Normalize(player.position - transform.position);

            // check if angle is less than 30
            if (Vector3.Angle(transform.forward, npcToPlayer) < viewAngle)
            {
                Ray ray = new(transform.position, npcToPlayer);

                // if ray hit
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        // set npc to chase
                        lastSeen = hit.point;
                        return true;
                    }
                }
                // otherwise continue on route
            }

            return false;
        }

        private void Update()
        {
            if (endGame) return;
            // update state machine
            _mStateMachine.Update();

            // make npc face direction of travel
            if (npc.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(npc.velocity.normalized), Time.deltaTime * rotationSpeed);
            }
        }

        public void EndGame()
        {
            _cm.GameController.Caught();
        }

        public void HearMeow(Vector3 pos)
        {
            if (_cm.GameController.debugMode)
            {
                Debug.Log("A meow was heard by " + gameObject);
            }

            npc.destination = pos;
            meowPosition = pos;
            meow = true;
        }
    }
}