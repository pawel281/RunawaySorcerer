using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerCombat : MagicCombatBase
{
  
    private CreateSpellUISelector _spellUiSelector;
    [SerializeReference] private Test[] test;
    [SerializeReference]Dictionary<string,string> test2=new Dictionary<string, string>();
    [Inject]
    private void Constructor(CreateSpellUISelector spellUiSelector)
    {
        _spellUiSelector = spellUiSelector;
    }

    public override void Cast()
    {
        if (!_spellUiSelector.gameObject.activeSelf)
        {
            base.Cast();
        }
    }


    

}

interface Test
{
    void Test();
}
[Serializable]
class  Test1: MonoBehaviour,Test
{
    public void Test()
    {
        throw new System.NotImplementedException();
    }
}
