using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class MagicSpellBase : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer;
    protected bool _isActive;
    protected MagicSpellData _spellData;
    public MagicSpellData SpellData => _spellData;
    public bool IsActive => _isActive;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void Initialize(MagicSpellData data);

    public abstract void Activate();

    public abstract void DestroyUnfinishedSpell();
    public abstract void DestroySpell();
}