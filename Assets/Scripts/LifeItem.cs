public class LifeItem : Item
{
    public float m_HealthAdded;
    public override void Pick(PlayerHealth Player)
    {
        if(Player.GetLife() < Player.m_MaxHealth)
        {
            Player.AddHealth(m_HealthAdded);
            Destroy(gameObject);
        }
    }
}