using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float JumpHeight = 1;
    public float IdleTime = 3000;

    private StateMachine _StateMachine;

    // Start is called before the first frame update
    void Start()
    {
        _StateMachine = new StateMachine(gameObject);

        Jump jumpState = new Jump(_StateMachine, JumpHeight);
        InAir inAirState = new InAir(_StateMachine);
        Idle idleState = new Idle(_StateMachine);

        HasJumped jumpedCondition = new HasJumped(_StateMachine);
        HasLanded landedCondition = new HasLanded(_StateMachine);
        TimeExpired timerCondition = new TimeExpired(_StateMachine, IdleTime);

        _StateMachine.AddState(idleState, true);
        _StateMachine.AddState(jumpState);
        _StateMachine.AddState(inAirState);
        
        _StateMachine.AddTransition(idleState, jumpState, timerCondition);
        _StateMachine.AddTransition(jumpState, inAirState, jumpedCondition);
        _StateMachine.AddTransition(inAirState, idleState, landedCondition);

    }

    // Update is called once per frame
    void Update()
    {
        _StateMachine.Update();
    }
}
