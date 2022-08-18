using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateSpellBase : MagicSpellBase
{    

    public override void Initialize(MagicSpellData data)
    {
        _spellData = Instantiate(data);
        _spriteRenderer.sprite = data.SpriteSpell;
        _spriteRenderer.color =data.ColorSpell;
    }

    public override void Activate()
    {
        Destroy(gameObject);
    }

    public override void DestroyUnfinishedSpell()
    {
        Destroy(gameObject);
    }

    public override void DestroySpell()
    {
        throw new System.NotImplementedException();
    }
}
