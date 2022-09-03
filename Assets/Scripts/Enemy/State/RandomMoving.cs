using UnityEngine;

namespace StateMachine
{
    public class RandomMoving : Moving
    {
        private Vector3 _randomPos;
        [SerializeField] private State _nextState;
        [SerializeField] private float _radiusRandomPosition;

        public override State RunCurrentState(AIStateController aiStateController)
        {
            if (_randomPos == Vector3.zero)
            {
                _randomPos = transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * _radiusRandomPosition;
                UpdatePath(_randomPos);
            }

            aiStateController.BotMovement.HandTurn((_randomPos - transform.position).normalized);
            if (Move(aiStateController.BotMovement))
            {
                _randomPos = Vector3.zero;
                return _nextState;
            }
            else
            {
                return this;
            }
        }
    }
}