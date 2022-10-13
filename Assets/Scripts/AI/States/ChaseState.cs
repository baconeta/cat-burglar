using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState<AIMovement> {
    // called on enter
    public override void Enter(AIMovement NPC) {

    }
    
    // called once per frame
    public override void Execute(AIMovement NPC) {
        // if player in view
        if(NPC.InView()) {
            // move to last known location
            NPC.MoveTo(NPC.LastSeen);
        } else {
            // change to search state
            NPC.ChangeState(new SearchState());
        }
    }
    
    // called on exit
    public override void Exit(AIMovement NPC) {
        
    }
}