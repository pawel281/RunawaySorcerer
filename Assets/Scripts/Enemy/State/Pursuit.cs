using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class Pursuit : Moving
{
    [SerializeField] private EnemyCombat _combat;
    [SerializeField] private float _radiusPursuit;
    [SerializeField] private float _radiusStopPursuit;
    [SerializeField] private float _radiusAttack;
    [SerializeField] [Range(0f, 1f)] private float _attackShieldMod;
    [SerializeField] private float _cdElementAdd;
    [SerializeField] private float _cdAfterCreation;
    [SerializeField] [Range(0f, 1f)] private float _chanceFail;
    [SerializeField] private State _idle;
    [SerializeField] private State _randomMoving;
    [SerializeField] private float _cdRandomMoving;
    private float _lastTimeRandomMoving;
    [SerializeField] [Range(0f, 1f)] private float _chanceRandomMoving;
    private GameObject _target;
    private Coroutine _updatePath;

    public override State RunCurrentState(AIStateController aiStateController)
    {
        if (!_target)
        {
            StopPursuit();
            return _idle;
        }

        if (Vector3.Distance(_target.transform.position, transform.position) > _radiusPursuit)
        {
            StopPursuit();
            return _idle;
        }
        else
        {
            if (Vector3.Distance(_target.transform.position, transform.position) <= _radiusAttack)
            {
                aiStateController.BotMovement.HandTurn((_target.transform.position - transform.position).normalized);
                if (_combat.StartCreateSpell(_attackShieldMod, _cdElementAdd, _cdAfterCreation, _chanceFail))
                {
                    _combat.Cast();
                }
            }

            if (Vector3.Distance(_target.transform.position, transform.position) > _radiusStopPursuit)
            {
                if (_updatePath == null)
                {
                    _updatePath = StartCoroutine(UpdatePathProcess());
                }

                if (Time.time > _lastTimeRandomMoving)
                {
                    _lastTimeRandomMoving = Time.time + _cdRandomMoving;
                    if (Random.value < _chanceRandomMoving)
                    {
                        StopPursuit();
                        return _randomMoving;
                    }
                }

                Move(aiStateController.BotMovement);
            }
            else
            {
                StopPursuit();
            }

            return this;
        }
    }

    private void StopPursuit()
    {
        if (_updatePath != null)
        {
            StopCoroutine(_updatePath);
            _updatePath = null;
        }
    }

    private IEnumerator UpdatePathProcess()
    {
        while (_target)
        {
            yield return new WaitForSeconds(1);
            UpdatePath(_target.transform.position);
        }

        _updatePath = null;
    }


    public void SetTarget(GameObject target)
    {
        _target = target;
    }
}