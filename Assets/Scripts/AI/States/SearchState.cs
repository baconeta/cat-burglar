using UnityEngine;

namespace AI.States
{
    public class SearchState : BaseState<AIMovement>
    {
        private Vector3 mTarget;
        private float mTimer;

        // called on enter
        public override void Enter(AIMovement NPC)
        {
            NPC.SetSpeed(2.0f);
            // last known player location
            mTarget = NPC.LastSeen;

             // move to target
            NPC.MoveTo(mTarget);
           

            // search for 5 seconds
            mTimer = 5;
        }

        // called once per frame
        public override void Execute(AIMovement NPC)
        {
            // if player in view
            if (NPC.InView(30.0f))
            {
                // chase
                NPC.ChangeState(new ChaseState());
            }
            else if (mTimer <= 0)
            {
                // change to patrol state
                NPC.ChangeState(new PatrolState());
            }
            else if(NPC.meow){
                Vector3 distance = NPC.MeowPosition - NPC.Position;
                distance.y = 0;
                if(distance.magnitude < 2f){
                    Debug.Log("we have entered the meow");
                    NPC.ChangeState(new SearchState());
                }
            }
            else
            {
                // if at last known location
                if (Vector3.Distance(NPC.Position, mTarget) < 0.5f)
                {
                    // generate random location nearby
                    mTarget += new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));

                    // move to target
                    NPC.MoveTo(mTarget);
                }
            }

            // countdown timer
            mTimer -= Time.deltaTime;
        }

        // called on Exit
        public override void Exit(AIMovement NPC)
        {
            NPC.meow = false;
        }
    }
}