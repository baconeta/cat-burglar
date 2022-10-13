using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine<T> {
    private T mOwner;
    private BaseState<T> mCurrentState;

    // constructor
    public BaseStateMachine(T owner, BaseState<T> initial) {
        mOwner = owner;
        mCurrentState = initial;

        if(mCurrentState != null) {
            mCurrentState.Enter(mOwner);
        }
    }

    // update statemachine
    public void Update() {
        if(mCurrentState != null) {
            mCurrentState.Execute(mOwner);
        }
    }

    // change state
    public void ChangeState(BaseState<T> next) {
        if(mCurrentState != null) {
            mCurrentState.Enter(mOwner);
        }
    
        mCurrentState = next;

        if(mCurrentState != null) {
            mCurrentState.Enter(mOwner);
        }
    }
}
