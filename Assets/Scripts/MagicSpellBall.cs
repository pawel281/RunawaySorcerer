using UnityEngine;

public abstract class MagicSpellBall : MonoBehaviour
{
    private Color _colorSpell;
    private float _damage;
    private float _speed;

    public Color ColorSpell => _colorSpell;
    public float Speed => _speed;
    public float Damage => _damage;

    protected float _lifeTime;

    public virtual void Initialize(Sprite spriteSpell, float damage, Color colorSpell, float speed, float lifeTimeSpell)
    {
        var _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = colorSpell;
        _spriteRenderer.sprite = spriteSpell;
        _damage = damage;
        _colorSpell = colorSpell;
        _speed = speed;
        _lifeTime = lifeTimeSpell;
    }

    public abstract void Activate();

    public  void CastDeactivation()
    {
        //партиклы добавить
        Destroy(gameObject);
    }
}