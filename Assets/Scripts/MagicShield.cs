using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicShield : MonoBehaviour
{
    private List<ShieldElement> _shieldElements;
    [SerializeField] private GameObject _elementVisual;


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
                _shieldElements.Add(new ShieldElement(data,this,yieldingSpell));
            }
        }
    }

    private class ShieldElement
    {
        private MagicShield _magicShield;
        private MagicSpellData _yieldingSpell;
        private bool isActive;
        private float shieldHp;
        private float startShieldHp;
        private Color _color;
        private SpriteRenderer _elementVisual;
        private Coroutine _lifeCycle;
        public MagicSpellData YieldingSpell => _yieldingSpell;


        public ShieldElement(MagicSpellData dataShield, MagicShield shield,MagicSpellData dataYielding)
        {
            _magicShield = shield;
            _yieldingSpell = dataYielding;
            _elementVisual = Instantiate(_magicShield._elementVisual, _magicShield.transform.position, _magicShield.transform.rotation).GetComponent<SpriteRenderer>();
            _elementVisual.color = _color = dataShield.ColorSpell;
            ActivateShield(dataShield);
        }

        public void ActivateShield(MagicSpellData data)
        {
            _elementVisual.gameObject.SetActive(true);
            startShieldHp = data.Life;
            shieldHp = startShieldHp;
            isActive = true;
            if (_lifeCycle != null)
            {
                _magicShield.StopCoroutine(_lifeCycle);
            }

            _lifeCycle = _magicShield.StartCoroutine(LifeСycle());
        }

        private IEnumerator LifeСycle()
        {
            while (shieldHp > 0f)
            {
                shieldHp -= Time.deltaTime;
                //сделать анимацию
                yield return 0;
            }

            _lifeCycle = null;
            isActive = false;
            _magicShield._elementVisual.gameObject.SetActive(false);
        }
    }
}