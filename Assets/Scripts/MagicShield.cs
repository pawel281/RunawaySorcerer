using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicShield : MonoBehaviour
{
    private List<ShieldElement> _shieldElements = new List<ShieldElement>();
    [SerializeField] private float _radiusElementMoving;
    [SerializeField] private float _speedElementMoving;
    [SerializeField] private GameObject _elementVisual;


    private void OnValidate()
    {
        if (!_elementVisual)
        {
            throw new InvalidOperationException();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var spell = other.GetComponent<MagicSpellBase>();
        if (spell && spell.IsActive)
        {
            var shieldElement = _shieldElements.FirstOrDefault(i => i.YieldingSpell.Name == spell.SpellData.Name);
            if (shieldElement != null)
            {
                shieldElement.ChangeShieldHp(shieldElement.ShieldHp - spell.SpellData.Damage);
                spell.DestroySpell();
            }
        }
    }

    public void AddShield(MagicSpellData data)
    {
        foreach (var yieldingSpell in data.YieldingSpells)
        {
            var element = _shieldElements.FirstOrDefault(i => i.YieldingSpell == yieldingSpell);
            if (element != null)
            {
                element.ActivateShield(data);
            }
            else
            {
                _shieldElements.Add(new ShieldElement(data, this, yieldingSpell));
            }
        }
    }

    private class ShieldElement
    {
        private MagicShield _magicShield;
        private MagicSpellData _yieldingSpell;
        private bool _isActive;
        private float _shieldHp;
        private float _startShieldHp;
        private Color _color;
        private SpriteRenderer _elementVisual;
        private Coroutine _lifeCycle;
        public MagicSpellData YieldingSpell => _yieldingSpell;
        public float ShieldHp => _shieldHp;

        public ShieldElement(MagicSpellData dataShield, MagicShield shield, MagicSpellData dataYielding)
        {
            _magicShield = shield;
            _yieldingSpell = dataYielding;
            _elementVisual = DiContainerRef.Container.InstantiatePrefab(_magicShield._elementVisual, _magicShield.transform.position, _magicShield.transform.rotation,_magicShield.transform).GetComponent<SpriteRenderer>();
            _elementVisual.color = _color = dataShield.ColorSpell;
            ActivateShield(dataShield);
        }

        public void ActivateShield(MagicSpellData data)
        {
            _elementVisual.gameObject.SetActive(true);
            _startShieldHp = data.Life;
            _shieldHp = _startShieldHp;
            _isActive = true;
             if (_lifeCycle != null)
             {
                 _magicShield.StopCoroutine(_lifeCycle);
             }
            _lifeCycle = _magicShield.StartCoroutine(LifeСycle());
        }

        public void ChangeShieldHp(float hp)
        {
            _shieldHp = hp;
            //анимация
        }

        private IEnumerator LifeСycle()
        {
            while (_shieldHp > 0f)
            {
                _shieldHp -= Time.deltaTime;
                _elementVisual.transform.localPosition = new Vector3(
                    Mathf.Sin(_shieldHp * _magicShield._speedElementMoving) * _magicShield._radiusElementMoving,
                    Mathf.Sin(_shieldHp * _magicShield._speedElementMoving) * _magicShield._radiusElementMoving * 0.5f,
                    Mathf.Cos(_shieldHp * _magicShield._speedElementMoving) * _magicShield._radiusElementMoving);
                yield return 0;
            }

            _elementVisual.gameObject.SetActive(false);
            _lifeCycle = null;
            _isActive = false;
            _magicShield._elementVisual.gameObject.SetActive(false);
        }
    }
}