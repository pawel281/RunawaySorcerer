using UnityEngine;

public abstract class MagicSpellBase : MonoBehaviour
{
    private Color _colorSpell;
    private float _damage;
    public Color ColorSpell => _colorSpell;
    private SpriteRenderer _spriteRenderer;
    private float _speed;
    private float _lifeTime;

  
    public virtual void Initialize(Sprite spriteSpell, float damage, Color colorSpell, float speed, float lifeTime)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = spriteSpell;
        _damage = damage;
        _colorSpell = colorSpell;
        _speed = speed;
        _lifeTime = lifeTime;
    }
    
    public abstract void Activate();
    
}


