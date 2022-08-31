using Zenject;

public class PlayerCombat : MagicCombatBase
{
  
    private CreateSpellUISelector _spellUiSelector;
    [Inject]
    private void Constructor(CreateSpellUISelector spellUiSelector)
    {
        _spellUiSelector = spellUiSelector;
    }

    public override void Cast()
    {
        if (!_spellUiSelector.gameObject.activeSelf)
        {
            base.Cast();
        }
    }



}