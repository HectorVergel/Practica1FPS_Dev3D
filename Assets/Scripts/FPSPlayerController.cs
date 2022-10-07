
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FPSPlayerController : MonoBehaviour
{

    float m_Yaw;
    float m_Pitch;
    public float m_Life = 5f;

    public float m_YawRotationSpeed;
    public float m_PitchRotationSpeed;

    public float m_MinPitch;
    public float m_MaxPitch;

    public float m_PlayerMass;

    public Transform pitchController;
    

    public bool yawInverted;
    public bool pitchInverted;

    private bool m_moving;

    public CharacterController m_CharacterController;
    public float m_Speed;
    [Header("Inputs")]
    public KeyCode m_LeftKey;
    public KeyCode m_RightKey;
    public KeyCode m_UpKey;
    public KeyCode m_DownKey;
    public KeyCode m_RunKeyCode = KeyCode.LeftShift;
    public KeyCode m_JumpKey = KeyCode.Space;

    [Header(" ")]
    float m_VerticalSpeed = 0.0f;
    bool m_OnGround = true;

    public float m_JumpSpeed = 10.0f;

    public float m_FastSpeedMultiplier = 1.5f;

    public Camera m_Camera;
    public float m_NormalSpeedFOV;
    public float m_FastSpeedFOV;
    public float m_IncreaseSpeedFOV;
    private float m_FOV;

    private float m_TimeOnAir;
    public float m_CoyoteTime = 0.0f;

    private Vector3 m_mov;


    [Header("Shoot")]
    public float m_MaxShootDistance = 50.0f;
    public LayerMask m_ShootingLayerMask;
    public GameObject m_decalPrefab;
    public float m_SpreadFactor;
    private float m_Spread;
    public float m_FireRate;
    private float m_FireTimer;
    private float m_TimeShooting;
    public float m_MaxRecoilRotation;
    public float m_MinRecoilRotation;
    public float m_SpeedRecoilRotation;
    public int m_MaxAmountBullets;
    private int m_CurrentBullets;

    

    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockKeyCode = KeyCode.O;
    bool m_AngleLocked = false;
    bool m_AimLocked = true;

    [Header("Animations")]
    public Animation m_MyAnimation;
    public AnimationClip m_IdleAnimation;
    public AnimationClip m_ShootAnimation;
    public AnimationClip m_ReloadAnimation;
    bool m_Shooting = false;
    void Start()
    {
        m_Yaw = transform.rotation.y;
        m_Pitch = pitchController.localRotation.x;
        m_FOV = m_NormalSpeedFOV;
        m_Life = GameController.GetGameController().GetPlayerLife();
        m_CurrentBullets = m_MaxAmountBullets;
        SetIdleWeaponAnimation();

    }

#if UNITY_EDITOR
    void Shortcuts()
    {
        if (Input.GetKeyDown(m_DebugLockAngleKeyCode))
            m_AngleLocked = !m_AngleLocked;
        if (Input.GetKeyDown(m_DebugLockKeyCode))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            m_AimLocked = Cursor.lockState == CursorLockMode.Locked;
        }
    }

#endif

    void Update()
    {
#if UNITY_EDITOR

        Shortcuts();
#endif

        Vector3 l_RightDirection = transform.right;
        Vector3 l_ForwardDirection = transform.forward;
        Vector3 l_Direction = Vector3.zero;
        float l_Speed = m_Speed;

        //Inputs
        if (Input.GetKey(m_UpKey))
        {
            l_Direction = l_ForwardDirection;
        }
        if (Input.GetKey(m_DownKey))
        {
            l_Direction = -l_ForwardDirection;
        }
        if (Input.GetKey(m_RightKey))
        {
            l_Direction += l_RightDirection;
        }
        if (Input.GetKey(m_LeftKey))
        {
            l_Direction -= l_RightDirection;
        }
        if (Input.GetKeyDown(m_JumpKey) && m_OnGround)
        {
            m_VerticalSpeed = m_JumpSpeed;
        }
        if (Input.GetKey(m_RunKeyCode))
        {
            l_Speed = m_Speed * m_FastSpeedMultiplier;

            if (m_FOV < m_FastSpeedFOV)
            {
                m_FOV += m_IncreaseSpeedFOV * Time.deltaTime;
            }


        }
        else
        {

            if (m_FOV > m_NormalSpeedFOV)
            {
                m_FOV -= m_IncreaseSpeedFOV * Time.deltaTime;
            }


        }

        CheckIfMoving(l_Direction);

        //Set FOV
        m_Camera.fieldOfView = Mathf.Clamp(m_FOV, m_NormalSpeedFOV, m_FastSpeedFOV);


        l_Direction.Normalize();

        PlayerRotation();

        //Move
        m_mov = l_Direction * l_Speed * Time.deltaTime;



        SetGravity();

        CollisionFlags l_collisionFlags = m_CharacterController.Move(m_mov);

        CheckCollision(l_collisionFlags);


        if (Input.GetMouseButton(0))
        {
            Shoot();
            m_TimeShooting += Time.deltaTime;
        }
        else
        {
            m_TimeShooting = 0.0f;
            m_Spread = 0.0f;
        }

        if (m_FireTimer < m_FireRate)
            m_FireTimer += Time.deltaTime;

    }


    bool CanShot()
    {
        return !m_Shooting;
    }

    void SetIdleWeaponAnimation()
    {
        m_MyAnimation.CrossFade(m_IdleAnimation.name);
    }

    void SetShootAnimation()
    {
        m_MyAnimation.CrossFade(m_ShootAnimation.name, 0.1f);
        m_MyAnimation.CrossFadeQueued(m_IdleAnimation.name, 0.1f);
        StartCoroutine(EndShoot());
    }

    void Shoot()
    {


        if (!CanShot() || m_CurrentBullets == 0) return;

        Vector3 l_ShootDirection = m_Camera.transform.forward;

        m_Spread += m_TimeShooting * m_SpreadFactor;
        l_ShootDirection.x += Random.Range(-m_Spread*4.0f, m_Spread*4.0f);
        l_ShootDirection.y += Random.Range(-m_Spread, m_Spread);

        //ShootRecoil();
        m_Shooting = true;
        SetShootAnimation();
        RaycastHit l_raycastHit;
        if (Physics.Raycast(m_Camera.transform.position, l_ShootDirection, out l_raycastHit, m_MaxShootDistance, m_ShootingLayerMask.value))
            CreateShootHitParticles(l_raycastHit.collider, l_raycastHit.point, l_raycastHit.normal);

        m_FireTimer = 0.0f;
        m_CurrentBullets--;



    }

   /* void ShootRecoil()
    {

        float l_recoilX = 0.0f;

        if(pitchController.localRotation.x > m_MinRecoilRotation)
        {
            Debug.Log("Recoil!");
            l_recoilX += m_SpeedRecoilRotation * Time.deltaTime;
            m_Camera.transform.localRotation = Quaternion.Euler(-l_recoilX, 0, 0);
        }

        if (pitchController.localRotation.x < m_MaxRecoilRotation)
        {
            Debug.Log("No recoil");
            l_recoilX -= m_SpeedRecoilRotation * Time.deltaTime;
            m_Camera.transform.localRotation = Quaternion.Euler(-l_recoilX, 0, 0);
        }
        
    }*/

    void CreateShootHitParticles(Collider _collider, Vector3 position, Vector3 normal)
    {
        Debug.DrawRay(position, normal, Color.red, 5f);

        Instantiate(m_decalPrefab, position, Quaternion.LookRotation(normal));
    }
    void SetGravity()
    {
        m_VerticalSpeed += Physics.gravity.y * Time.deltaTime - m_PlayerMass;
        m_mov.y = m_VerticalSpeed * Time.deltaTime;
    }

    void PlayerRotation()
    {
        float l_MouseX = Input.GetAxis("Mouse X");
        float l_MouseY = Input.GetAxis("Mouse Y");
#if UNITY_EDITOR
        if (m_AngleLocked)
        {
            l_MouseX = 0.0f;
            l_MouseY = 0.0f;
        }
#endif

        m_Yaw += m_YawRotationSpeed * l_MouseX * Time.deltaTime * (yawInverted ? -1f : 1f);
        m_Pitch += m_PitchRotationSpeed * l_MouseY * Time.deltaTime * (pitchInverted ? -1f : 1f);
        m_Pitch = Mathf.Clamp(m_Pitch, m_MinPitch, m_MaxPitch);



        transform.rotation = Quaternion.Euler(0.0f, m_Yaw, 0.0f);
        pitchController.localRotation = Quaternion.Euler(m_Pitch, 0.0f, 0.0f);
    }



    void CheckCollision(CollisionFlags collisionFlag)
    {
        if ((collisionFlag & CollisionFlags.Above) != 0 && m_VerticalSpeed > 0.0f)
        {
            m_VerticalSpeed = 0.0f;
        }

        if ((collisionFlag & CollisionFlags.Below) != 0)
        {
            m_VerticalSpeed = 0.0f;
            m_TimeOnAir = 0.0f;
            m_OnGround = true;
        }
        else
        {
            m_TimeOnAir += Time.deltaTime;
            if (m_TimeOnAir > m_CoyoteTime)
                m_OnGround = false;
        }
    }

    void CheckIfMoving(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            m_moving = true;
        }
        else
        {
            m_moving = false;
        }
    }

    IEnumerator EndShoot()
    {
        yield return new WaitForSeconds(m_ShootAnimation.length);
        m_Shooting = false;
    }

}