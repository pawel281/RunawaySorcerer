using System;
using UnityEngine;

public class AIStateController : MonoBehaviour
{
    private State _currentState;
    private GameObject _target;
    private void Awake()
    {
        throw new NotImplementedException();
    }

    private void RunStateMachine()
    {
        State nextState = _currentState?.RunCurrentState();
        if (nextState)
        {
            SwitchNextState(nextState);
        }
    }

    private void SwitchNextState(State nexState)
    {
        _currentState = nexState;
    }

    private void Update()
    {
        RunStateMachine();
    }
}