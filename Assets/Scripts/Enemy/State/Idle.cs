using UnityEngine;

namespace StateMachine
{
    public class Idle : State
    {
        [SerializeField] private State _nextState;

        public override State RunCurrentState(AIStateController aiStateController)
        {
            aiStateController.BotMovement.ChangeDirection(Vector3.zero);
            return this;
        }

        private void SearchTarget()
        {
        }
    }
}