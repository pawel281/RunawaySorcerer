using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class MagicSpellBall : MonoBehaviour
{
    protected SpriteRenderer _sprite;
    

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public abstract void Initialize(MagicSpellData data);
    
    public abstract void Activate();

    public  abstract void CastDeactivation();
}