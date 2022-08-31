using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCombat : MagicCombatBase
{
    [SerializeField] private MagicSpellData[] _attackMagicSpells;
    [SerializeField] private MagicSpellData[] _protectMagicSpells;
    private Coroutine _createSpell;

    protected override void Awake()
    {
        base.Awake();
        _AllMagicSpells = _attackMagicSpells.Concat(_protectMagicSpells).ToArray();
    }

    private IEnumerator StartCreateSpellProcess(MagicSpellData spell, float cdElementAdd, float cdAfterCreation, float chanceFail)
    {
        foreach (var magicElement in spell.Composition)
        {
            yield return new WaitForSeconds(cdElementAdd);
            if (Random.value < chanceFail)
            {
                AddMagicElementToPool(magicElement);
            }
            else
            {
                _currentSpell?.DestroyUnfinishedSpell();
                break;
            }
        }
        yield return new WaitForSeconds(cdAfterCreation);
        _createSpell = null;
    }

    public void StartCreateSpell(float attackShieldMod, float cdElementAdd, float cdAfterCreation, float chanceFail)
    {
        if (_createSpell == null && !_currentSpell)
        {
            MagicSpellData spell;
            if (Random.value < attackShieldMod)
            {
                spell = _attackMagicSpells[Random.Range(0, _attackMagicSpells.Length)];
            }
            else
            {
                spell = _protectMagicSpells[Random.Range(0, _protectMagicSpells.Length)];
            }

            _createSpell = StartCoroutine(StartCreateSpellProcess(spell, cdElementAdd, cdAfterCreation, chanceFail));
        }
    }
}