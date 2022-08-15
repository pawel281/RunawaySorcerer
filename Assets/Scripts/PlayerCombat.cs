using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MagicCombatBase
{
    [SerializeField] private MagicElement[] testElements; //удалить полсе теста

    [SerializeField] private Transform spellPoint;
    private List<MagicElement> _poolElements = new List<MagicElement>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) //test
        {
            AddMagicElementToPool(testElements[0]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            AddMagicElementToPool(testElements[1]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            AddMagicElementToPool(testElements[2]);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           Cast();
        }
    }

    public override void Cast()
    {
        _previousSpell?.Activate();
        _poolElements.Clear();
        _previousSpell = null;
    }

    public override void CreateSpell()
    {
        var spell = CheckOverlapSpell();
        _previousSpell?.CastDeactivation();
        if (spell)
        {
            if (_poolElements.Count == spell.Composition.Length)
            {
                _previousSpell = spell.CreateSpellObject(spellPoint);
            }
            else
            {
                _previousSpell = _magicSpellIntermediate.CreateSpellObject(spellPoint, CombineColors(_poolElements.Select(i => i.Color).ToArray()));
            }
        }
        else
        {
            _poolElements.Clear();
            _previousSpell = null;
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