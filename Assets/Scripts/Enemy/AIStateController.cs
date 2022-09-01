using System;
using UnityEngine;

namespace StateMachine
{
    public class AIStateController : MonoBehaviour
    {
        private State _currentState;
        private Movement _botMovement;//может вынести отдельно
        private GameObject _target;

        public Movement BotMovement => _botMovement;

        private void Awake()
        {
//        throw new NotImplementedException();
        }

        private void RunStateMachine()
        {
            State nextState = _currentState?.RunCurrentState(this);
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
}