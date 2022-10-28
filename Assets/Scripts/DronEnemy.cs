using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class DronEnemy : IEnemy
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
    NavMeshAgent m_NavMeshAgent;
    public List<Transform> m_PatrolTargets;
    int m_CurrentPatrolTargetID = 0;

    public float m_HearRange;
    public float m_ConeVisualAngle;
    public float m_SightDistance = 8.0f;
    public LayerMask m_SightLayer;
    public float m_EyesHeight = 1.0f;
    public float m_EyesPlayerHeight = 1.0f;

    public float m_MaxHealth;
    private float m_CurrentHealth;

    public float m_RotationSpeed = 10.0f;
    public float m_DistanceChase = 5.0f;
    public float m_MaxDistanceChase = 12.0f;
    public float m_ShootMax = 7.0f;

    public float m_StartRotation;
    public float m_EndRotation;
    float m_YRotation;

    public GameObject m_DronBullet;
    public Transform m_ShootPoint;
    public float m_FireRate;
    private float m_TimeToShoot;

    public Animation m_MyAnimation;
    public AnimationClip m_DieAnimationDrone;
    public AnimationClip m_IdleAnimationDrone;

    public Image m_LifeBarImage;
    public Transform m_LifeBarAnchorPosition;
    public RectTransform m_LifeBarRectTransform;

    public GameObject m_DieEffect;
    public Transform m_EffectPositionDrone;

    public float m_StunTime;
    IState m_PreviousState;
    bool m_IsDead = false;

    bool m_Hitted = false;
    public ParticleSystem m_ShootParticle;





    private void Start()
    {
        m_IsDead = false;
        SetIdleState();
        m_CurrentHealth = m_MaxHealth;
        m_LifeBarImage.fillAmount = m_CurrentHealth;

    }
    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        switch (m_State)
        {
            case IState.IDLE:
                UpdateIdleState();
                break;
            case IState.PATROL:
                UpdatePatrolState();
                break;
            case IState.ALERT:
                UpdateAlertState();
                break;
            case IState.CHASE:
                UpdateChaseState();
                break;
            case IState.ATTACK:
                UpdateAttackState();
                break;
            case IState.HIT:
                UpdateHitState();
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
        SetPatrolState();

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

        RotateDron();


    }
    //NO HACER CORUTINA

    void RotateDron()
    {
        m_NavMeshAgent.isStopped = true;


        if (m_YRotation < m_EndRotation)
        {
            m_YRotation += m_RotationSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_YRotation, transform.eulerAngles.z);
            if (SeePlayer())
            {

                SetChaseState();

            }

        }
        else
        {
            SetPatrolState();
        }


    }
    
    bool PatrolTargetPositionArrived()
    {
        return !m_NavMeshAgent.hasPath && !m_NavMeshAgent.pathPending && m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete;
    }

    void MoveToNextPatrolPosition()
    {
        ++m_CurrentPatrolTargetID;
        if (m_CurrentPatrolTargetID >= m_PatrolTargets.Count)
            m_CurrentPatrolTargetID = 0;
        m_NavMeshAgent.destination = m_PatrolTargets[m_CurrentPatrolTargetID].position;
    }

    bool HearsPlayer()
    {
        Vector3 l_PlayerPosition = GameController.GetGameController().GetPlayer().transform.position;
        return Vector3.Distance(l_PlayerPosition, transform.position) <= m_HearRange && GameController.GetGameController().GetPlayer().m_moving;
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
    void SetPatrolState()
    {
        m_State = IState.PATROL;
        m_NavMeshAgent.isStopped = false;
        m_NavMeshAgent.destination = m_PatrolTargets[m_CurrentPatrolTargetID].position;
    }
    void UpdatePatrolState()
    {
        m_NavMeshAgent.isStopped = false;
        if (PatrolTargetPositionArrived())
        {
            MoveToNextPatrolPosition();
        }
        if (HearsPlayer() && m_State != IState.ALERT)
        {

            SetAlertState();
        }
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
            DronShoot();
        }
        else
        {
            SetChaseState();
        }

    }

    void DronShoot()
    {
        Vector3 l_ShootDirection = GameController.GetGameController().GetPlayer().transform.position - transform.position;
        l_ShootDirection.Normalize();

        m_TimeToShoot += Time.deltaTime;

        if (m_TimeToShoot >= m_FireRate)
        {
            m_ShootParticle.Play();
            GameObject go = Instantiate(m_DronBullet, m_ShootPoint.position, Quaternion.identity);
            go.GetComponent<Bullet>().SetBulletDirection(l_ShootDirection);
            m_TimeToShoot = 0.0f;
        }
    }

    void SetHitState(IState state)
    {
        if(state != IState.HIT)
        {
            m_PreviousState = state;
        }
        
        m_State = IState.HIT;
    }
    void UpdateHitState()
    {
        
        StartCoroutine(HitDelay(m_PreviousState));
    }

    void SetDieState()
    {
        
        StartCoroutine(DieDrone());
        m_State = IState.DIE;

    }
    void UpdateDieState()
    {
        
       
    }

    IEnumerator DieDrone()
    {
        if (!m_IsDead)
        {
            Instantiate(m_DieEffect, m_EffectPositionDrone.position, Quaternion.identity);
            SetDieAnimation();
            m_IsDead = true;
        }
        yield return new WaitForSeconds(m_DieAnimationDrone.length);
        gameObject.SetActive(false);
    }

    void SetDieAnimation()
    {
        
        m_MyAnimation.CrossFade(m_DieAnimationDrone.name, 0.1f);
    }

    void SetIdleAnimation()
    {
        m_MyAnimation.CrossFade(m_IdleAnimationDrone.name, 0.1f);
    }

    void SetChaseState()
    {
        StopAllCoroutines();
        m_State = IState.CHASE;
    }
    void UpdateChaseState()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {

        m_NavMeshAgent.isStopped = false;
        Vector3 l_PlayerPosition = GameController.GetGameController().GetPlayer().transform.position;
        m_NavMeshAgent.destination = l_PlayerPosition;
        transform.LookAt(l_PlayerPosition);

        if (Vector3.Distance(transform.position, l_PlayerPosition) <= m_DistanceChase)
        {
            m_NavMeshAgent.isStopped = true;
            SetAttackState();
        }
        else
        {
            m_NavMeshAgent.isStopped = false;
        }

        if (Vector3.Distance(transform.position, l_PlayerPosition) >= m_MaxDistanceChase)
        {
            
            SetPatrolState();
        }

    }

    public void Hit(float _damage)
    {
        if (!m_IsDead)
        {
            m_Hitted = true;
            if (m_CurrentHealth > 0.0f)
            {
                m_CurrentHealth -= _damage;
                m_LifeBarImage.fillAmount = m_CurrentHealth / m_MaxHealth;

                StartCoroutine(DroneHitCouldown());
                SetHitState(m_State);
                
                

            }
            if (m_CurrentHealth <= 0.0f)
            {
                StopAllCoroutines();
                SetDieState();
            }
            
           
        }
       
    }
    IEnumerator DroneHitCouldown()
    {

        yield return new WaitForSeconds(2f);
        m_Hitted = false;
    }
   

    IEnumerator HitDelay(IState state)
    {

        if (m_Hitted)
        {
            yield return new WaitForSeconds(m_StunTime);
        }
        
       
        if(state == IState.PATROL || state == IState.IDLE)
        {
            SetAlertState();
        }
        else
        {
            m_State = state;
        }
        StartCoroutine(DroneHitCouldown());
       
    }

    

    public override void RestartGame()
    {
        m_State = IState.IDLE;
        m_CurrentHealth = m_MaxHealth;
        m_IsDead = false;
        m_Hitted = false;
        m_LifeBarImage.fillAmount = 1.0f;
        SetIdleAnimation();
        gameObject.SetActive(true);
    }
}
