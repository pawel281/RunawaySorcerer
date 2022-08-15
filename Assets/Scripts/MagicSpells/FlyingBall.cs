using UnityEngine;

public class FlyingBall : MagicSpellBall
{
    private float _damage;
    private float _speed;
    protected float _lifeTime;
    protected Color _colorSpell;

   
    public float Speed => _speed;
     public Color ColorSpell => _colorSpell;
     public float Damage => _damage;


     public override void Initialize(MagicSpellData data)
     {
      
         _damage =data.Damage;
         _colorSpell = data.ColorSpell;
         _speed = data.Speed;
         _lifeTime = data.LifeTime;
         _sprite.sprite = data.SpriteSpell;
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
