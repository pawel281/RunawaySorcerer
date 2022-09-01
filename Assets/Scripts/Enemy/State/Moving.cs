using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using UnityEngine;


namespace StateMachine
{
    public class Moving : State
    {
        private List<Vector3> _pointsPath = new List<Vector3>();

        public override State RunCurrentState(AIStateController aiStateController)
        {
            throw new System.NotImplementedException();
        }

        protected bool Move(Movement _movement)
        {
            if (_pointsPath.Count > 0)
            {
                var nextPos = _pointsPath[0];
                if (Vector3.Distance(transform.position, nextPos) < 0.3f)
                {
                    _pointsPath.Remove(nextPos);
                }
                else
                {
                    _movement.ChangeDirection(nextPos - transform.position);
                    _movement.Move();
                }

                return false;
            }

            return true;
        }


        protected void UpdatePath(Vector3 pos)
        {
            NavMesh.SamplePosition(pos, out NavMeshHit navMeshPos, 1.0f, NavMesh.AllAreas);
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, navMeshPos.position, NavMesh.AllAreas, path))
            {
                _pointsPath = path.corners.ToList();
            }
        }
    }
}