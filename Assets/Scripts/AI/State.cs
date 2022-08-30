using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract State RunCurrentState();
}
