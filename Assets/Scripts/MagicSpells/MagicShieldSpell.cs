using Zenject;

public class MagicShieldSpell : MagicSpellBase
{
    private MagicShield _magicShield;



    public override void Initialize(MagicSpellData data)
    {
        _spellData = Instantiate(data);
        _spriteRenderer.sprite = data.SpriteSpell;
        _spriteRenderer.color = data.ColorSpell;
        _magicShield = transform.root.GetComponentInChildren<MagicShield>();
    }

    public override void Activate()
    {
        _magicShield.AddShield(_spellData);
        DestroySpell();
    }

    public override void DestroyUnfinishedSpell()
    {
        Destroy(gameObject);
    }

    public override void DestroySpell()
    {
      Destroy(gameObject);
    }
}