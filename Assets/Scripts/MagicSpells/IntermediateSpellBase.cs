using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateSpellBase : MagicSpellBase
{    
    protected Color _colorSpell;
    public override void Initialize(MagicSpellData data)
    {
        _colorSpell = data.ColorSpell;
        _spriteRenderer.sprite = data.SpriteSpell;
        _spriteRenderer.color = _colorSpell;
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
