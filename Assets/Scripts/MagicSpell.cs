using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class MagicSpell : MonoBehaviour
{
    protected SpriteRenderer _sprite;
    protected bool _isActive;

    public bool IsActive => _isActive;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public abstract void Initialize(MagicSpellData data);
    
    public abstract void Activate();

    public  abstract void CastDeactivation();
}