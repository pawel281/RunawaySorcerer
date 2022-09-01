using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _hp;
    private float _startHp;
    public float Hp => _hp;
    public float StartHp => _startHp;
    public UnityAction OnChangeHp;

    private void Awake()
    {
        _startHp = _hp;
    }


    private void OnCollisionEnter(Collision other)
    {
        var magicSpell = other.transform.GetComponent<MagicSpellBase>();
        if (magicSpell)
        {
            ChangeHp(_hp-magicSpell.SpellData.Damage);  
        }
    }

    public void ChangeHp(float hp)
    {
        _hp = Mathf.Clamp(hp, 0, _startHp);
        OnChangeHp?.Invoke();
        if (_hp <= 0)
        {
           GetComponent<IDead>()?.OnDead();
        }
    }
}

interface IDead
{
    void OnDead();
}
