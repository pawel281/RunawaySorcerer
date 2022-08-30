using UnityEngine;

[CreateAssetMenu(menuName = "Magic/MagicSpellData")]
public class MagicSpellData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _spriteSpell;
    [SerializeField] private Color _colorSpell;
    [SerializeField] private string _description;
    [SerializeField] private GameObject _spellPrefab;
    [SerializeField] private float _damage; 
    [SerializeField] private float _speed;
    [SerializeField] private float _life;
    [SerializeField] protected MagicElement[] _composition;
    [SerializeField] private MagicSpellData[] _yieldingSpells;
    public string Name => _name;
    public Sprite SpriteSpell => _spriteSpell;
    public Color ColorSpell => _colorSpell;
    public string Description => _description;
    public GameObject SpellPrefab => _spellPrefab;
    public MagicElement[] Composition => _composition;
    public float Speed => _speed;
    public float Life => _life;
    public MagicSpellData[] YieldingSpells => _yieldingSpells;

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }


    public MagicSpellBase CreateSpellObject(Transform castPoint, Color? col = null)
    {
        if (col != null)
        {
            _colorSpell = (Color) col;
        }

        var _castedSpell = DiContainerRef.Container.InstantiatePrefab(_spellPrefab, castPoint.position, _spellPrefab.transform.rotation,castPoint);
        var magicSpell = _castedSpell.GetComponent<MagicSpellBase>();
        magicSpell.Initialize(this);
     
        return magicSpell;
    
    }
}


