using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MagicCombatBase : MonoBehaviour
{
    [SerializeField] protected MagicSpellData[] _AllMagicSpells;
    protected MagicSpellBase _currentSpell;
    [SerializeField] protected MagicSpellData _magicSpellIntermediate;
    protected float _manna;
    [SerializeField] protected float _maxManna;
    [SerializeField] private float _speedMannaRegen;
    [SerializeField] protected Transform _spellPoint;
    protected List<MagicElement> _poolElements = new List<MagicElement>();

    public UnityAction<bool> onElementAdded;
    public float SpeedMannaRegen => _speedMannaRegen;
    public MagicSpellBase CurrentSpell => _currentSpell;


    protected virtual void Awake()
    {
        _manna = _maxManna;
    }

    private void OnValidate()
    {
        if (!_magicSpellIntermediate)
            throw new InvalidOperationException();
        if (!_spellPoint)
            throw new InvalidOperationException();
    }

    protected virtual void Update()
    {
        ChangeManna(_manna + Time.deltaTime * _speedMannaRegen);
    }

    protected void CreateSpell()
    {
        var spell = CheckOverlapSpell();
        _currentSpell?.DestroyUnfinishedSpell();
        if (spell)
        {
            if (_poolElements.Count == spell.Composition.Length)
            {
                _currentSpell = spell.CreateSpellObject(_spellPoint);
            }
            else
            {
                _currentSpell = _magicSpellIntermediate.CreateSpellObject(_spellPoint, MagicSpellBase.CombineSpellsColors(_poolElements.Select(i => i.Color).ToArray()));
            }

            onElementAdded?.Invoke(false);
        }
        else
        {
            _poolElements.Clear();
            _currentSpell = null;
            onElementAdded?.Invoke(true);
        }
    }

    protected MagicSpellData CheckOverlapSpell()
    {
        List<MagicSpellData> _spells = new List<MagicSpellData>();
        var OverlapCount = 0;
        foreach (var i in _AllMagicSpells.Where(i => i.Composition.Length >= _poolElements.Count).ToArray())
        {
            var OverlapTempCount = 0;
            var overlap = true;
            for (var j = 0; j < _poolElements.Count; j++)
            {
                if (_poolElements[j] == i.Composition[j])
                {
                    OverlapTempCount++;
                }
                else
                {
                    overlap = false;
                    break;
                }
            }

            if (!overlap)
            {
                continue;
            }

            if (OverlapTempCount == OverlapCount)
            {
                _spells.Add(i);
            }

            if (OverlapTempCount > OverlapCount)
            {
                OverlapCount = OverlapTempCount;
                _spells.Clear();
                _spells.Add(i);
            }
        }

        if (_spells.Count > 0)
        {
            return _spells.OrderBy(i => i.Composition).ToArray()[0];
        }

        return null;
    }

    public virtual void ChangeManna(float value)
    {
        _manna = Mathf.Clamp(value, 0, _maxManna);
    }

    public virtual void Cast()
    {
        _currentSpell?.Activate();
        _poolElements.Clear();
        _currentSpell = null;
    }

    public virtual void AddMagicElementToPool(MagicElement magicElement)
    {
        if (_manna > magicElement.MannaCost)
        {
            _poolElements.Add(magicElement);
            CreateSpell();
            ChangeManna(_manna - magicElement.MannaCost);
        }
    }

    public virtual void ChangeMannaRegenSpeedSpeed(float speed)
    {
        _speedMannaRegen = Mathf.Clamp(speed, 0, Mathf.Infinity);
    }
}