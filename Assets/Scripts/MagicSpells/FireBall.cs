using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MagicSpellBase
{
    public override void Activate()
    {
       Destroy(gameObject);
    }
}
