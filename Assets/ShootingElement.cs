using System.Collections;
using UnityEngine;

public class ShootingElement : MonoBehaviour
{
    public GameObject m_Effect;
    public Transform m_EffectPoint;
    public int m_ScoreAdded;
    public ShootingGallery m_GalleryHUD;
    public void OnBulletHit()
    {
        if (m_Effect != null)
        {
            Instantiate(m_Effect, m_EffectPoint.position, Quaternion.identity);
        }
        m_GalleryHUD.AddScore(m_ScoreAdded);
        this.gameObject.SetActive(false);
    }



}
