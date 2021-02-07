using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeExpired : Condition
{
    private float _StartTime;
    private float _CurrentTime;

    public TimeExpired(StateMachine stateMachine, float time) : base(stateMachine)
    {
        _StartTime = time;
    }

    public override void OnEnter()
    {
        _CurrentTime = _StartTime;
    }

    public override bool CheckCondition()
    {
        _CurrentTime -= Time.deltaTime;

        return _CurrentTime <= 0;
    }
}
