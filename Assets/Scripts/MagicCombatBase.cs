using System;
using UnityEngine;

public abstract class MagicCombatBase : MonoBehaviour
{
    [SerializeField] protected MagicSpellData[] _AllMagicSpells;
    protected MagicSpellBase _currentSpell;
    [SerializeField] protected MagicSpellData _magicSpellIntermediate;
    protected float _manna;
    [SerializeField] protected float _maxManna;
    [SerializeField] private float _speedMannaRegen;
    public float SpeedMannaRegen => _speedMannaRegen;
    public MagicSpellBase CurrentSpell => _currentSpell;

    private void Awake()
    {
        _manna = _maxManna;
    }

    protected  virtual void ChangeManna(float value)
    {
        _manna = Mathf.Clamp(value, 0, _maxManna);
    }

    protected virtual void Update()
    {
        ChangeManna(_manna + Time.deltaTime * _speedMannaRegen);
    }

    protected Color CombineSpellsColors(Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 1);
        foreach (Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;

        return result;
    }

    public virtual void  ChangeMannaRegenSpeedSpeed(float speed)
    {
        _speedMannaRegen =  Mathf.Clamp(speed,0,Mathf.Infinity);

    }
    public abstract void Cast();

    public abstract void CreateSpell();
}