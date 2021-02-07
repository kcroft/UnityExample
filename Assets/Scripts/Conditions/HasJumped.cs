using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasJumped : Condition
{
    public HasJumped(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override bool CheckCondition()
    {
        Jump jumpState = GetStateMachine().GetCurrentState<Jump>();

        if (jumpState != null)
        {
            return jumpState.GetJumpCount() > 0;
        }

        return false;
    }
}
