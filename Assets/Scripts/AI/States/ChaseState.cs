using UnityEngine;

namespace AI.States
{
    public class ChaseState : BaseState<AIMovement> {

        //private bool isColliding = false;
        // called on enter
        public override void Enter(AIMovement NPC) {
            NPC.SetSpeed(5.0f);
        }
        
        // called once per frame
        public override void Execute(AIMovement NPC) {
            
                // if player in view
            if (NPC.InView(180.0f)) {
                // move to last known location
                NPC.MoveTo(NPC.LastSeen);

                Vector3 distance = NPC.PlayerPosition - NPC.Position;
                distance.y = 0;

                if (distance.magnitude < 0.5f) {
                    Debug.Log("CAUGHT YA");
                    // end game
                    AIMovement.EndGame();
                }
            }
            else {
                // change to search state
                NPC.ChangeState(new SearchState());
            }

        }

            // called on exit
        public override void Exit(AIMovement NPC){

        }
    }

}