using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CreateSpellUISelector : View
{
    [SerializeField] private Image _iconSpell;
    [SerializeField] private TMP_Text _nameSpell;
    [SerializeField] private TMP_Text _descriptionSpell;
    private PlayerCombat _playerCombat;

[Inject]
    private void Constructor(PlayerCombat playerCombat)
    {
        _playerCombat =playerCombat;
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
    public override void Initialize()
    {
        UpdateUI();
        _playerCombat.elementAdded += UpdateUI;
      
    }

    private void OnEnable()
    {
        UpdateUI();
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

    private void OnDestroy()
    {
        _playerCombat.elementAdded -= UpdateUI;
    }
}