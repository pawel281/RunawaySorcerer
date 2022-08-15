using System;
using UnityEngine;

public abstract class MagicCombatBase : MonoBehaviour
{
  
    [SerializeField] protected MagicSpellData[] _AllMagicSpells;
    protected MagicSpell _previousSpell;
    [SerializeField] protected MagicSpellData _magicSpellIntermediate;
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

  
    public Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0,0,0,1);
        foreach(Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;

        return result;
    }

}
