using UnityEngine;
public class KeyItem : Item
{

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
        Player.m_HaveKey = true;
        Destroy(gameObject);
    }
}