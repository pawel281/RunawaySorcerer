using System;
using UnityEngine;

public abstract class State : ScriptableObject
{
    protected Movement _movement;
    protected GameObject _target;


    private void Awake()
    {
        
    }

    public abstract State RunCurrentState();

   

}
