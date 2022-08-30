using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateController : MonoBehaviour
{
    private State _currentState;
 
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

  private  void Update()
    {
        RunStateMachine();
    }
}
