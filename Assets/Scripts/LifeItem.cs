
using UnityEngine;

public class LifeItem : Item
{
    public float m_HealthAdded;
    public float m_DistanceUI;
    public GameObject m_UIPrefab;

    public override void Update()
    {
        if (Vector3.Distance(transform.position, GameController.GetGameController().GetPlayer().transform.position) <= m_DistanceUI)
        {
            m_UIPrefab.SetActive(true);
            m_UIPrefab.transform.LookAt(Camera.main.transform.position, Vector3.up);
        }
        else
        {
            m_UIPrefab.SetActive(false);

        }
    }
    public override void Pick(FPSPlayerController Player)
    {
        if(Player.m_PlayerHealth.GetLife() < Player.m_PlayerHealth.m_MaxHealth)
        {
            Player.m_PlayerHealth.AddHealth(m_HealthAdded);
            Destroy(gameObject);
        }
    }
}
