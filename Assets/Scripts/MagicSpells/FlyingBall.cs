using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlyingBall : MagicSpellBase
{
    private Rigidbody _rigidbody;
    private Vector3 _direction;


    public override void Initialize(MagicSpellData data)
    {
        _spellData = Instantiate(data);
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        _spriteRenderer.sprite = data.SpriteSpell;
        _spriteRenderer.color = data.ColorSpell;
    }

    public override void Activate()
    {
        _isActive = true;
        _rigidbody.isKinematic = false;
        _direction = transform.parent.up;
        transform.parent = null;
        StartCoroutine(TimerDestroy());
    }

    public override void DestroyUnfinishedSpell()
    {
        Destroy(gameObject);
    }

    public override void DestroySpell()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            _rigidbody.velocity = _direction * _spellData.Speed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isActive)
        {
            var otherSpell = other.transform.GetComponent<MagicSpellBase>();
            if (otherSpell != null && otherSpell.IsActive)
            {
                if (_spellData.YieldingSpells.FirstOrDefault(i => i.Name == otherSpell.SpellData.Name)) //затычка
                {
                    otherSpell.DestroySpell();
                    if (otherSpell.SpellData.YieldingSpells.FirstOrDefault(i => i.Name == _spellData.Name))
                    {
                        DestroySpell();
                    }
                }
            }
            else
            {
                DestroySpell();
            }
        }
    }

    private IEnumerator TimerDestroy()
    {
        yield return new WaitForSeconds(_spellData.Life);
        DestroySpell();
    }
}