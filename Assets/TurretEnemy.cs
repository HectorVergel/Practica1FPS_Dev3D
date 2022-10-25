using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretEnemy : IEnemy
{
    public enum IState
    {
        IDLE = 0,
        PATROL,
        ALERT,
        CHASE,
        ATTACK,
        HIT,
        DIE

    }
   
    public IState m_State;

    public float m_ConeVisualAngle;
    public float m_SightDistance = 8.0f;
    public LayerMask m_SightLayer;
    public float m_EyesHeight = 1.0f;
    public float m_EyesPlayerHeight = 1.0f;

    public float m_MaxHealth;
    private float m_CurrentHealth;

    public float m_RotationSpeed = 10.0f;
    public float m_ShootMax = 7.0f;

    public float m_StartRotation;
    public float m_EndRotation;
    float m_YRotation;

    public GameObject m_DronBullet;
    public GameObject m_ShootEffect;
    public Transform m_ShootPoint;
    public float m_FireRate;
    private float m_TimeToShoot;
    

    public Image m_LifeBarImage;
    public Transform m_LifeBarAnchorPosition;
    public RectTransform m_LifeBarRectTransform;

    public GameObject m_DieEffect;
    public Transform m_EffectPositionDrone;

    public float m_StunTime;
    bool m_IsDead = false;





    private void Start()
    {
        m_IsDead = false;
        SetIdleState();
        m_CurrentHealth = m_MaxHealth;
        m_LifeBarImage.fillAmount = m_CurrentHealth;

    }
    private void Awake()
    {
       
    }
    private void Update()
    {
        
        switch (m_State)
        {
            case IState.IDLE:
                UpdateIdleState();
                break;
               
            case IState.ALERT:
                UpdateAlertState();
                break;
           
            case IState.ATTACK:
                UpdateAttackState();
                break;
           
            case IState.DIE:
                UpdateDieState();
                break;

        }


        Vector3 l_PlayerPosition = GameController.GetGameController().GetPlayer().transform.position;
        Vector3 l_EyesPosition = transform.position + Vector3.up * m_EyesHeight;
        Vector3 l_PlayerEyesPosition = l_PlayerPosition + Vector3.up * m_EyesPlayerHeight;
        Debug.DrawLine(l_EyesPosition, l_PlayerEyesPosition, SeePlayer() ? Color.red : Color.blue);
    }

    void SetIdleState()
    {
        m_State = IState.IDLE;
    }
    void UpdateIdleState()
    {
       SetAlertState();

    }

    void SetAlertState()
    {
        m_StartRotation = transform.eulerAngles.y;
        m_EndRotation = m_StartRotation + 360f;
        m_YRotation = m_StartRotation;
        m_State = IState.ALERT;

    }
    void UpdateAlertState()
    {

        RotateTurret();


    }
   

    void RotateTurret()
    {
       


        if (m_YRotation < m_EndRotation)
        {
            m_YRotation += m_RotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_YRotation, transform.eulerAngles.z);
            if (SeePlayer())
            {

               SetAttackState();

            }

        }
       


    }

  

   

    bool SeePlayer()
    {
        Vector3 l_PlayerPosition = GameController.GetGameController().GetPlayer().transform.position;
        Vector3 l_DirectionToPlayerXZ = l_PlayerPosition - transform.position;
        l_DirectionToPlayerXZ.y = 0.0f;
        l_DirectionToPlayerXZ.Normalize();

        Vector3 l_ForwardXZ = transform.forward;
        l_ForwardXZ.y = 0.0f;
        l_ForwardXZ.Normalize();

        Vector3 l_EyesPosition = transform.position + Vector3.up * m_EyesHeight;
        Vector3 l_PlayerEyesPosition = l_PlayerPosition + Vector3.up * m_EyesPlayerHeight;
        Vector3 l_Direction = l_PlayerEyesPosition - l_EyesPosition;

        float l_Length = l_Direction.magnitude;
        l_Direction /= l_Length;
        Ray l_Ray = new Ray(l_EyesPosition, l_PlayerEyesPosition);


        return Vector3.Distance(l_PlayerPosition, transform.position) < m_SightDistance
            && Vector3.Dot(l_ForwardXZ, l_DirectionToPlayerXZ) > Mathf.Cos(m_ConeVisualAngle * Mathf.Deg2Rad / 2.0f)
            && !Physics.Raycast(l_Ray, l_Length, m_SightLayer);
    }
 


    void SetAttackState()
    {
        m_State = IState.ATTACK;
    }
    void UpdateAttackState()
    {
        Vector3 l_PlayerPosition = GameController.GetGameController().GetPlayer().transform.position;
        transform.LookAt(l_PlayerPosition);
        if (Vector3.Distance(transform.position, l_PlayerPosition) <= m_ShootMax)
        {
            TurretShoot();
        }
        else 
        {
            SetAlertState();
        }
       
    }

    void TurretShoot()
    {
        Vector3 l_ShootDirection = GameController.GetGameController().GetPlayer().transform.position - transform.position;
        l_ShootDirection.Normalize();

        m_TimeToShoot += Time.deltaTime;

        if (m_TimeToShoot >= m_FireRate)
        {
            GameObject go = Instantiate(m_DronBullet, m_ShootPoint.position, Quaternion.identity);
            go.GetComponent<Bullet>().SetBulletDirection(l_ShootDirection);
            Instantiate(m_ShootEffect, m_ShootPoint.position, Quaternion.identity);
            m_TimeToShoot = 0.0f;
        }
    }

   
  

    void SetDieState()
    {

        StartCoroutine(DieTurret());
        m_State = IState.DIE;

    }
    void UpdateDieState()
    {


    }

    IEnumerator DieTurret()
    {
        if (!m_IsDead)
        {
            Instantiate(m_DieEffect, m_EffectPositionDrone.position, Quaternion.identity);
            
            m_IsDead = true;
        }
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

  
 
   
   
    public void Hit(float _damage)
    {
        if (!m_IsDead)
        {
            if (m_CurrentHealth > 0.0f)
            {
                m_CurrentHealth -= _damage;
                m_LifeBarImage.fillAmount = m_CurrentHealth / 100.0f;

                



            }
            if (m_CurrentHealth <= 0.0f)
            {
                StopAllCoroutines();
                SetDieState();
            }


        }

    }



    public override void RestartGame()
    {
        SetAlertState();
        m_CurrentHealth = m_MaxHealth;
        m_IsDead = false;
        m_LifeBarImage.fillAmount=1.0f;
        gameObject.SetActive(true);
    }


}
