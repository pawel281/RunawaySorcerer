
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateSpellUISelector :View
{
    [SerializeField] private Image _iconSpell;
    [SerializeField] private TMP_Text _nameSpell;
    [SerializeField] private TMP_Text _descriptionSpell;
    private PlayerCombat _playerCombat;
    public override void Initialize()
    {
        _iconSpell.enabled = false;
    }

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void UpdateUI()
    {
        if (_playerCombat.PreviousSpell)
        {
            
        }
    }
}
