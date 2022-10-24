using UnityEngine;

namespace AI.States
{
    public class SearchState : BaseState<AIMovement>
    {
        private Vector3 _mTarget;
        private float _mTimer;

        private Vector3 _distance;

        // called on enter
        public override void Enter(AIMovement NPC)
        {
            NPC.SetSpeed(2.0f);
            // last known player location
            _mTarget = NPC.lastSeen;

            // move to target
            NPC.MoveTo(_mTarget);

            // init distance vector
            _distance = new Vector3(99999, 99999, 99999);


            // search for 5 seconds
            _mTimer = 5;
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
            else if (_mTimer <= 0)
            {
                // change to patrol state
                NPC.ChangeState(new PatrolState());
                NPC.meow = false;
            }
            else if (NPC.meow)
            {
                Vector3 distance = NPC.meowPosition - NPC.Position;
                distance.y = 0;
            }
            else
            {
                // if at last known location
                if (Vector3.Distance(NPC.Position, _mTarget) < 0.5f)
                {
                    // generate random location nearby
                    _mTarget += new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));

                    // move to target
                    NPC.MoveTo(_mTarget);
                }
            }

            if (_distance.magnitude < 1f)
            {
                Debug.Log("We have entered the meow");
                NPC.ChangeState(new SearchState());
            }

            // countdown timer
            _mTimer -= Time.deltaTime;
        }

        // called on Exit
        public override void Exit(AIMovement NPC)
        {
            NPC.meow = false;
        }
    }
}