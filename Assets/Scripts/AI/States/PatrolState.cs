using UnityEngine;

namespace AI.States
{
    public class PatrolState : BaseState<AIMovement>
    
    {
         private float mTimer;
         Vector3 distance;
        // called on enter
        public override void Enter(AIMovement NPC)
        {
            NPC.SetSpeed(2.0f);
            // move to initial point
            NPC.MoveTo(NPC.PatrolPoint);
            mTimer = 5;
            // init distance vector
            distance = new Vector3(99999,99999,99999);
        }

        // called once per frame
        public override void Execute(AIMovement NPC)
        {
            //Vector3 d = Vector3.Distance(NPC.Position, NPC.PatrolPoint);
            Vector3 d = NPC.Position - NPC.PatrolPoint;
            d.y = 0;
            // if npc at point
            if (d.magnitude < 0.1f)
            {
                // move to next
                NPC.MoveTo(NPC.MoveNext());
            }

            // if player in view
            if (NPC.InView(30.0f))
            {
                // change to chase state
                NPC.ChangeState(new ChaseState());
            }

            if(NPC.meow){
                distance = NPC.MeowPosition - NPC.Position;
                distance.y = 0;

            }

            if(distance.magnitude < 1f){
                Debug.Log("we have entered the meow");
                NPC.ChangeState(new SearchState());
            } 
            mTimer -= Time.deltaTime;

        }

        // called on exit
        public override void Exit(AIMovement NPC)
        {
            NPC.meow = false;
        }
    }
}