using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StateMachine
{
    struct Transition
    {
        private State _ToState;
        private Condition _Condition;

        public Transition(State toState, Condition condition)
        {
            _ToState = toState;
            _Condition = condition;
        } 

        public Condition GetCondition()
        {
            return _Condition;
        }

        public State GetToState()
        {
            return _ToState;
        }
    }

    private GameObject _Obj;
    private State _CurrentState;
    private Dictionary<State, List<Transition>> _States;

    public StateMachine(GameObject obj)
    {
        _States = new Dictionary<State, List<Transition>>();
        _Obj = obj;
    }

    public void Update()
    {
        if (_CurrentState != null)
        {
            _CurrentState.Update();

            foreach (Transition transition in _States[_CurrentState])
            {
                if (transition.GetCondition().CheckCondition())
                {
                    ChangeState(transition.GetToState());
                    break;
                }
            }
        }
    }

    private void ChangeState(State newState)
    {
        if (newState == null)
        {
            return;
        }

        Debug.Log("Changing State to " + newState.GetType().Name);

        if (_CurrentState != null)
        {
            _CurrentState.OnExit();
        }
        _CurrentState = newState;

        _CurrentState.OnEnter();

        foreach (Transition transition in _States[_CurrentState])
        {
            transition.GetCondition().OnEnter();
        }
    }

    public void AddState(State state, bool isInitial = false)
    {
        _States.Add(state, new List<Transition>());

        if (isInitial)
        {
            ChangeState(state);
        }
    }

    public void AddTransition(State fromState, State toState, Condition condition)
    {
        if (_States.ContainsKey(fromState))
        {
            _States[fromState].Add(new Transition(toState, condition));
        }
        else
        {
            Debug.LogError("Attempting to add a state that does not exist on the state machine.");
        }
    }

    public T GetCurrentState<T>() where T : State
    {
        Assert.IsTrue(_CurrentState is T);
        return _CurrentState as T;
    }

    public GameObject GetGameObject()
    {
        return _Obj;
    }
}
