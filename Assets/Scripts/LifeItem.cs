public class LifeItem : Item
{
    public float m_HealthAdded;
    public override void Pick(FPSPlayerController Player)
    {
        if(Player.m_PlayerHealth.GetLife() < Player.m_PlayerHealth.m_MaxHealth)
        {
            Player.m_PlayerHealth.AddHealth(m_HealthAdded);
            Destroy(gameObject);
        }
    }
}
