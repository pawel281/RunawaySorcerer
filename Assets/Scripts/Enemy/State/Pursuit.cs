using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class Pursuit : Moving
{
    [SerializeField] private float _radiusPursuit;
    [SerializeField] private float _radiusAttack;
    
   
    public override State RunCurrentState(AIStateController aiStateController)
    {
      //  StartCreateSpell()
        throw new System.NotImplementedException();
    }
}
