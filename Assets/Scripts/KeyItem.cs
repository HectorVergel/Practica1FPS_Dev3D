public class KeyItem : Item
{
    public override void Pick(FPSPlayerController Player)
    {
        Player.m_HaveKey = true;
        Destroy(gameObject);
    }
}