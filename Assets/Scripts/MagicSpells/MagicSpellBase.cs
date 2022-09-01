using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class MagicSpellBase : MonoBehaviour
{
    protected SpriteRenderer _spriteRenderer;
    protected bool _isActive;
    protected MagicSpellData _spellData;
    protected Transform _startParent;
    public MagicSpellData SpellData => _spellData;
    public bool IsActive => _isActive;
    public Transform StartParent => _startParent;

    private void Awake()
    {
        _startParent = transform.root;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        var spellCollider = GetComponent<Collider>();
        if (transform.parent && spellCollider)
        {
            Physics.IgnoreCollision(spellCollider, _startParent.GetComponent<Collider>());
        }
    }

    public static Color CombineSpellsColors(Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 1);
        foreach (Color c in aColors)
        {
            result += c;
        }

        result /= aColors.Length;

        return result;
    }

    public abstract void Initialize(MagicSpellData data);

    public abstract void Activate();

    public abstract void DestroyUnfinishedSpell();
    public abstract void DestroySpell();
}