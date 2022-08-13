using System;
using UnityEngine;

public abstract class MagicCombatBase : MonoBehaviour
{
  
    [SerializeField] protected MagicSpellData[] _AllMagicSpells;
    protected MagicSpellData _previousSpell;
    protected float _manna;
    [SerializeField]  protected float _maxManna;


    private void Awake()
    {
        _manna = _maxManna;
    }
    protected void ChangeManna(float value)
    {
        _manna = Mathf.Clamp( value, 0, _maxManna);
    }
    public abstract void Cast();
    
    public abstract void CreateSpell();

  

}
