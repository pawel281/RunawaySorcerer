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
            if (Random.value > chanceFail)
            {
                AddMagicElementToPool(magicElement);
            }
            else
            {
                if (_currentSpell)
                {
                    _currentSpell.DestroyUnfinishedSpell();
                }

                break;
            }
        }

        yield return new WaitForSeconds(cdAfterCreation);
        _createSpell = null;
    }

    /// <summary>
    /// Запускает создание спела и возвращет тру, если создан
    /// </summary>
    /// <param name="attackShieldMod"> коэффициент атаки </param>
    /// <param name="cdElementAdd"> кд добавление элемента</param>
    /// <param name="cdAfterCreation"> кд задержки после создания</param>
    /// <param name="chanceFail"> Шансс неудачи</param>
    /// <returns></returns>
    public bool StartCreateSpell(float attackShieldMod, float cdElementAdd, float cdAfterCreation, float chanceFail)
    {
        if (_createSpell == null)
        {
            if (!_currentSpell)
            {
                MagicSpellData spell;
                if (Random.value > attackShieldMod)
                {
                    spell = _attackMagicSpells[Random.Range(0, _attackMagicSpells.Length)];
                }
                else
                {
                    spell = _protectMagicSpells[Random.Range(0, _protectMagicSpells.Length)];
                }

                _createSpell = StartCoroutine(StartCreateSpellProcess(spell, cdElementAdd, cdAfterCreation, chanceFail));
                return false;
            }
            else
            {
                return true;
            }
        }

        return false;
    }
}