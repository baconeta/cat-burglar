using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState<AIMovement> {
    // called on enter
    public override void Enter(AIMovement NPC) {
        // move to initial point
        NPC.MoveTo(NPC.PatrolPoint);
    }

    // called once per frame
    public override void Execute(AIMovement NPC) {

        //Vector3 d = Vector3.Distance(NPC.Position, NPC.PatrolPoint);
        Vector3 d = NPC.Position - NPC.PatrolPoint;
        d.y = 0;
        // if npc at point
        if(d.magnitude < 0.1f) {
            // move to next
            NPC.MoveTo(NPC.MoveNext());
        }

        // if player in view
        if(NPC.InView(30.0f)) {
            // change to chase state
            NPC.ChangeState(new ChaseState());
        }
    }

    // called on exit
    public override void Exit(AIMovement NPC) {

    }
}
