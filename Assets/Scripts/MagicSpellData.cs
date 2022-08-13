using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Magic/MagicSpellData")]
public class MagicSpellData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _spriteSpell;
    [SerializeField] private Color _colorSpell;
    [SerializeField] private string _description;
    [SerializeField] private GameObject _spellPrefab;
    [SerializeField] protected MagicElement[] _composition;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    
    public string Name => _name;
    public Sprite SpriteSpell => _spriteSpell;
    public Color ColorSpell => _colorSpell;
    public string Description => _description;
    public GameObject SpellPrefab => _spellPrefab;
    public MagicElement[] Composition => _composition;
    public float Speed => _speed;
    public float LifeTime => _lifeTime;

  

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }

    public bool CheckPrescription(MagicElement[] magicalElements)
    {
        if (_composition.Length == 0)
            throw new InvalidOperationException();
        return _composition.Equals(magicalElements);
    }

    public void CreateSpellObject(Transform castPoint)
    {
        var _castedSpell = Instantiate(_spellPrefab, castPoint);
        _castedSpell.GetComponent<MagicSpellBall>().Initialize(_spriteSpell, _damage, _colorSpell,_speed,_lifeTime);
    }
}