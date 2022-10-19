
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [Header("Shoot")]
    public GameObject m_BulletPrefab;
    public int m_WeaponDamage;
    bool m_Shooting;
    public Transform m_FirePoint;
    public Camera m_Camera;
    public GameObject m_ShootCanonEffect;
    public LayerMask m_ShootingLayerMask;
    public GameObject m_decalPrefab;
    public float m_SpreadFactor;
    public float m_FireRate;
    public int m_MaxAmountBullets;
    public int m_MaxChargerBullets;
    private int m_CurrentBullets;


    [Header("Render")]
    public GameObject m_WeaponVisuals;
    public Transform m_WeaponPointVisuals;

    [Header("Animation")]
    public AnimationClip m_ShootAnimation;
    public AnimationClip m_ReloadAnimation;
    public Animation m_MyAnimation;
    public AnimationClip m_IdleAnimation;


    private void Start()
    {
        m_CurrentBullets = m_MaxChargerBullets;
        FPSPlayerController.OnReload.Invoke(m_CurrentBullets, m_MaxAmountBullets);

    }
    public void Reload()
    {
        m_MaxAmountBullets = m_MaxAmountBullets - (m_MaxChargerBullets - m_CurrentBullets);
        m_MaxAmountBullets = (int)Mathf.Clamp(m_MaxAmountBullets, 0, m_MaxAmountBullets);
        m_CurrentBullets = m_MaxChargerBullets;
        m_CurrentBullets = (int)Mathf.Clamp(m_CurrentBullets, 0, m_MaxChargerBullets);
        FPSPlayerController.OnReload.Invoke(m_CurrentBullets, m_MaxAmountBullets);
        SetReloadAnimation();

    }

    public void Shoot()
    {

        if (!CanShot() || m_CurrentBullets == 0) return;

        GameObject particle = Instantiate(m_ShootCanonEffect, m_FirePoint.position, Quaternion.identity);
        particle.transform.SetParent(m_FirePoint);

        Vector3 l_ShootDirection = m_Camera.transform.forward;

        FindObjectOfType<FPSPlayerController>().m_Spread += FindObjectOfType<FPSPlayerController>().m_TimeShooting * m_SpreadFactor;
        l_ShootDirection.x += Random.Range(-FindObjectOfType<FPSPlayerController>().m_Spread * 4.0f, FindObjectOfType<FPSPlayerController>().m_Spread * 4.0f);
        l_ShootDirection.y += Random.Range(-FindObjectOfType<FPSPlayerController>().m_Spread, FindObjectOfType<FPSPlayerController>().m_Spread);

        GameObject go = Instantiate(m_BulletPrefab, m_Camera.transform.position, Quaternion.identity);
        go.GetComponent<Bullet>().SetBulletDirection(l_ShootDirection);

        m_Shooting = true;
        SetShootAnimation();

        FindObjectOfType<FPSPlayerController>().m_FireTimer = 0.0f;
        m_CurrentBullets--;
        FPSPlayerController.OnReload.Invoke(m_CurrentBullets, m_MaxAmountBullets);

    }

    void SetShootAnimation()
    {

        m_MyAnimation.CrossFade(m_ShootAnimation.name, 0.1f);
        m_MyAnimation.CrossFadeQueued(m_IdleAnimation.name, 0.1f);
        StartCoroutine(EndShoot());
    }


    void SetReloadAnimation()
    {
        m_MyAnimation.CrossFade(m_ReloadAnimation.name, 0.1f);
        m_MyAnimation.CrossFadeQueued(m_IdleAnimation.name, 0.1f);
    }
    bool CanShot()
    {
        return !m_Shooting;
    }

     public void AddAmmo(int amount)
    {
        m_MaxAmountBullets += amount;
        FPSPlayerController.OnPickAmmo.Invoke(m_MaxAmountBullets);
    }
    IEnumerator EndShoot()
    {
        yield return new WaitForSeconds(m_ShootAnimation.length);
        m_Shooting = false;
    }
}
