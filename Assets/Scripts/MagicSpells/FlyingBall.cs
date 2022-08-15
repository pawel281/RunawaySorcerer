using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyingBall : MagicSpell
{
    private float _damage;
    private float _speed;
    private Rigidbody _rigidbody;
    private Vector3 _direction;
    protected float _lifeTime;
    protected Color _colorSpell;


    public float Speed => _speed;
    public Color ColorSpell => _colorSpell;
    public float Damage => _damage;


    public override void Initialize(MagicSpellData data)
    {

        _damage = data.Damage;
        _colorSpell = data.ColorSpell;
        _speed = data.Speed;
        _lifeTime = data.LifeTime;
        _sprite.sprite = data.SpriteSpell;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;

    }
    

    public override void Activate()
    {
        _isActive = true;
        _rigidbody.isKinematic = false;
        _direction = transform.parent.up;
        transform.parent = null;
        StartCoroutine(TimerDestroy());
        
    }
    

    public override void CastDeactivation()
    {
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (_isActive)
        {
            _rigidbody.velocity = _direction* _speed;
        }
    }

    private IEnumerator TimerDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        CastDeactivation();
    }
}
  
