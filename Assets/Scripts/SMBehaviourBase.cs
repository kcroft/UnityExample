using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMBehaviourBase
{
    private StateMachine _StateMachine;

    public SMBehaviourBase(StateMachine stateMachine)
    {
        _StateMachine = stateMachine;
    }

    protected StateMachine GetStateMachine()
    {
        return _StateMachine;
    }

    protected GameObject GetGameObject()
    {
        return _StateMachine.GetGameObject();
    }
}
