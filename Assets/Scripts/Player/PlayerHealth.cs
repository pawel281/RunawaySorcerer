
public class PlayerHealth : Health
{
    protected override void OnDead()
    {
        Destroy(gameObject);
        //та та та
    }
}
