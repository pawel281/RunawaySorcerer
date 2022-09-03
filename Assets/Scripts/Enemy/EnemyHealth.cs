using UnityEngine;

public class EnemyHealth : Health
{
    protected override void OnDead()
    {
      Destroy(gameObject);
      Debug.Log("килл");
    }
}
