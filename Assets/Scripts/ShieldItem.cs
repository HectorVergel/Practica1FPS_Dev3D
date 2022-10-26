using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : Item
{
    public float m_ShieldAdded;
    public float m_DistanceUI;
    public GameObject m_UIPrefab;
    public override void Pick(FPSPlayerController Player)
    {
        if (Player.m_PlayerHealth.GetShield() < Player.m_PlayerHealth.m_MaxShield)
        {
            Player.m_PlayerHealth.AddShield(m_ShieldAdded);
            gameObject.SetActive(false);

        }
    }

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

}
