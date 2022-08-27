using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerCombat : MagicCombatBase
{
    [SerializeField] private MagicElement[] testElements; //удалить полсе теста
    [SerializeField] private Transform _spellPoint;
    private List<MagicElement> _poolElements = new List<MagicElement>();
    private CreateSpellUISelector _spellUiSelector;
    public UnityAction<bool> elementAdded;
    [Inject] 
    private void Constructor(CreateSpellUISelector spellUiSelector)
    {
        _spellUiSelector = spellUiSelector;
    }

    private void OnValidate()
    {
        if (!_magicSpellIntermediate)
            throw new InvalidOperationException();
        if (!_spellPoint)
            throw new InvalidOperationException();
    }


    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Alpha1)) //test
        {
            AddMagicElementToPool(testElements[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddMagicElementToPool(testElements[1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddMagicElementToPool(testElements[2]);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cast();
        }
    }

    public override void Cast()
    {
        if (!_spellUiSelector.gameObject.activeSelf)
        {
            _currentSpell?.Activate();
            _poolElements.Clear();
            _currentSpell = null;
        }
    }

    public override void CreateSpell()
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
                _currentSpell = _magicSpellIntermediate.CreateSpellObject(_spellPoint, CombineSpellsColors(_poolElements.Select(i => i.Color).ToArray()));
            }
            elementAdded?.Invoke(false);
        }
        else
        {
            _poolElements.Clear();
            _currentSpell = null;
            elementAdded?.Invoke(true);
        }
     
    }

    public void AddMagicElementToPool(MagicElement magicElement)
    {
        if (_manna > magicElement.MannaCost)
        {
            _poolElements.Add(magicElement);
            CreateSpell();
            ChangeManna(_manna - magicElement.MannaCost);
          
        }
    }

    private MagicSpellData CheckOverlapSpell()
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
}