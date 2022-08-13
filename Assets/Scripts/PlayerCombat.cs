using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MagicCombatBase
{

    [SerializeField] private MagicElement[] testElements;//удалить полсе теста
    private  List<MagicElement> _poolElements;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            
        }
    }

    public override void Cast()
    {
        throw new System.NotImplementedException();
    }

    public override void CreateSpell()
    {
        var spell = _AllMagicSpells.FirstOrDefault(i => i.Composition.Equals(_poolElements));
        print();
    }

    public void AddMagicElementToPool(MagicElement magicElement)
    {
        if (_manna > magicElement.MannaCost)
        {
            _poolElements.Add(magicElement);
            CreateSpell();
            ChangeManna(_manna- magicElement.MannaCost);
        }
    }
}
