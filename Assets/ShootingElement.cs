using System.Collections;
using UnityEngine;

public class ShootingElement : MonoBehaviour
{
    public GameObject m_Effect;
    public Transform m_EffectPoint;
    public void OnBulletHit()
    {
        if (m_Effect != null)
        {
            Instantiate(m_Effect, m_EffectPoint.position, Quaternion.identity);
        }
        this.gameObject.SetActive(false);
    }



}
