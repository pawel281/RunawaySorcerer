using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CreateSpellUISelector : View
{
    [SerializeField] private Image _iconSpell;
    [SerializeField] private Image _iconPlace;
    [SerializeField] private TMP_Text _nameSpell;
    [SerializeField] private TMP_Text _descriptionSpell;
    private PlayerCombat _playerCombat;

    [Inject]
    private void Constructor(PlayerCombat playerCombat)
    {
        _playerCombat = playerCombat;
    }

    public override void Initialize()
    {
        UpdateUI();
        _playerCombat.elementAdded += ElementAdded;
    }

    private void OnValidate()
    {
        if (_iconSpell == null)
            throw new InvalidOperationException();
        if (_nameSpell == null)
            throw new InvalidOperationException();
        if (_descriptionSpell == null)
            throw new InvalidOperationException();
    }

    private void OnEnable()
    {
        UpdateUI();
        _iconPlace.color = Color.white;
    }

    private void OnDestroy()
    {
        _playerCombat.elementAdded -= ElementAdded;
    }


    private void UpdateUI()
    {
        if (_playerCombat.CurrentSpell)
        {
            _iconSpell.enabled = true;
            var spellData = _playerCombat.CurrentSpell.SpellData;
            _iconSpell.sprite = spellData.SpriteSpell;
            _iconSpell.color = spellData.ColorSpell;
            _nameSpell.text = spellData.Name;
            _descriptionSpell.text = spellData.Description;
        }
        else
        {
            _iconSpell.enabled = false;
            _nameSpell.text = "";
            _descriptionSpell.text = "";
        }
    }

    private void ElementAdded(bool isFailed)
    {
        StartCoroutine(IconAnimation(isFailed?Color.red:Color.green));
        UpdateUI();
    }

    private void SuccessfulCast()
    {
        StartCoroutine(IconAnimation(Color.green));
    }

    private IEnumerator IconAnimation(Color col)
    {
        var t = 0f;
        var startColor = _iconPlace.color;
        while (t < 1f)
        {
            _iconPlace.color = Color.Lerp(startColor, col, t);
            t += Time.deltaTime * 8;
            yield return 0;
        }

        while (t > 0f)
        {
            _iconPlace.color = Color.Lerp(startColor, col, t);
            t -= Time.deltaTime * 8;
            yield return 0;
        }

        _iconPlace.color = Color.white;
    }
}