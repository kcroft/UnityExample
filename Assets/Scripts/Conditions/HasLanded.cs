using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasLanded : Condition
{
    public HasLanded(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override bool CheckCondition()
    {
        InAir inAirState = GetStateMachine().GetCurrentState<InAir>();

        if (inAirState != null)
        {
            return inAirState.IsGrounded();
        }

        return false;
    }
}
