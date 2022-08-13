using UnityEngine;

public class FireBall : MagicSpellBall
{
    public override void Activate()
    {
       Destroy(gameObject);
    }
}
