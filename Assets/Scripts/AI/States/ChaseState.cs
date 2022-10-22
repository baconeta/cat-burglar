using UnityEngine;

namespace AI.States
{
    public class ChaseState : BaseState<AIMovement> {

        //private bool isColliding = false;
        private float mTimer;
        // called on enter
        public override void Enter(AIMovement NPC) {
            NPC.uhoh.Play();
            NPC.SetSpeed(5.0f);
            mTimer = 5;
        }
        
        // called once per frame
        public override void Execute(AIMovement NPC) {
            
                // if player in view
            if (NPC.InView(180.0f)) {

                
                // move to last known location
                NPC.MoveTo(NPC.LastSeen);

                Vector3 distance = NPC.PlayerPosition - NPC.Position;
                distance.y = 0;

                if (distance.magnitude < 0.9f) {
                    Debug.Log("Player caught");
                    // end game
                    NPC.endGame = true;
                    NPC.EndGame();
                    NPC.ChangeState(new PatrolState());
                }

            }
            else {
                // change to search state
                NPC.ChangeState(new SearchState());
            }

            // so player doesnt get stuck in a shelf
            // if ai cannot reach player after 5 seconds, ai will give up and move on
            if(mTimer <= 0){
                NPC.ChangeState(new PatrolState());
            }

            mTimer -= Time.deltaTime;

        }

            // called on exit
        public override void Exit(AIMovement NPC){
        }
    }

}