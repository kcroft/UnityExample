using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : SMBehaviourBase
{
    public Condition(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public virtual void OnEnter()
    {
        
    }

    public virtual bool CheckCondition()
    {
        return false;
    }
}
