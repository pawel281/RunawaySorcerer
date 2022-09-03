using UnityEngine;
using Zenject;

namespace StateMachine
{
    public class Idle : State
    {
        [SerializeField] private Pursuit _pursuitState;
        private PlayerCombat _target; //затычка  пока на прямую
        [SerializeField] private float _distanceVision;

        [Inject]
        private void Constructor(PlayerCombat player)
        {
            _target = player;
        }

        public override State RunCurrentState(AIStateController aiStateController)
        {
            aiStateController.BotMovement.ChangeDirection(Vector3.zero);
            if (SearchTarget())
            {
                _pursuitState.SetTarget(_target.gameObject);
                return _pursuitState;
            }

            return this;
        }

        private bool SearchTarget() //пока затычка 
        {
            if (_target)
            {
                return Vector3.Distance(_target.transform.position, transform.position) < _distanceVision;
            }

            return false;
        }
    }
}