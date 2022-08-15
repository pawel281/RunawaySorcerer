using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateSpell : MagicSpell
{    
    protected Color _colorSpell;
    public override void Initialize(MagicSpellData data)
    {
        _colorSpell = data.ColorSpell;
        _sprite.sprite = data.SpriteSpell;
        _sprite.color = _colorSpell;
    }

    public override void Activate()
    {
        Destroy(gameObject);
    }

    public override void CastDeactivation()
    {
        Destroy(gameObject);
    }
}
