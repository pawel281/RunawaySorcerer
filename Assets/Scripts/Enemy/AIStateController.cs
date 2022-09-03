using System;
using UnityEngine;

namespace StateMachine
{
    public class AIStateController : MonoBehaviour
    {
        [SerializeField] private State _currentState;
        [SerializeField] private Movement _botMovement; //может вынести отдельно
        public Movement BotMovement => _botMovement;


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