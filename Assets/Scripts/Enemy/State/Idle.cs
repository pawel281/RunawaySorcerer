using UnityEngine;

public class Idle : State
{
    [SerializeField] private State _nextState;

    public override State RunCurrentState()
    {
        _movement.ChangeDirection(Vector3.zero);
        return this;
    }

    private void CheckPlayer()
    {
        
    }
}