using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InterfaceManager : MonoBehaviour
{
    public TextMeshProUGUI m_CurrentBulletInterfaceText;
    public TextMeshProUGUI m_MaxBulletInterfaceText;
    public TextMeshProUGUI m_HealthInterfaceText;
    public TextMeshProUGUI m_ShieldInterfaceText;

    public Image m_HealthImage;
    public Image m_ShieldImage;
    public Image m_Crosshair;
    public Image m_DieImage;
    public GameObject m_RifleImage;
    public GameObject m_PistolImage;
    public Color m_EnemyCrosshairColor;
    public Color m_CrosshairColor;

    public Animation m_MyAnimationHealth;
    public Animation m_MyAnimationShield;
    public AnimationClip m_HealthClip;
    public AnimationClip m_ShieldClip;

    public Camera m_MainCamera;
    public LayerMask m_LayerMask;

    public GameObject m_ButtonRetry;

    public float m_AlphaSpeed;

    public delegate void FadeOutFn();
    private void Start()
    {
        GameController.GetGameController().SetInterface(this);
    }

    private void Update()
    {
        UpdateCrosshair();
    }
    private void OnEnable()
    {
        FPSPlayerController.OnReload += UpdateBulletInterface;
        FPSPlayerController.OnPickAmmo += UpdateBulletInterface;
        PlayerHealth.OnHealthChange += UpdateHealthInterface;
        PlayerHealth.OnShieldChange += UpdateShieldInterface;
    }

    private void OnDisable()
    {
        FPSPlayerController.OnReload -= UpdateBulletInterface;
        FPSPlayerController.OnPickAmmo -= UpdateBulletInterface;
        PlayerHealth.OnHealthChange -= UpdateHealthInterface;
        PlayerHealth.OnShieldChange -= UpdateShieldInterface;

    }
    public void UpdateBulletInterface(int _currentBullets, int _maxBullets)
    {
        m_CurrentBulletInterfaceText.text = _currentBullets.ToString();
        m_MaxBulletInterfaceText.text =  _maxBullets.ToString();
    }

    public void UpdateBulletInterface(int _maxBullets)
    {
        
        m_MaxBulletInterfaceText.text =  _maxBullets.ToString();
    }

    public void UpdateHealthInterface(float _currentHealth)
    {
        m_HealthInterfaceText.text = ((int)_currentHealth).ToString();
        m_HealthImage.fillAmount = _currentHealth / 100.0f;
        m_MyAnimationHealth.Play(m_HealthClip.name);
    }

    public void UpdateShieldInterface(float _currentShield)
    {
        m_ShieldInterfaceText.text = ((int)_currentShield).ToString();
        m_ShieldImage.fillAmount = _currentShield / 100.0f;
        m_MyAnimationShield.Play(m_ShieldClip.name);
    }

    public void RestartGame()
    {
        GameControllerData l_GameControllerData = Resources.Load<GameControllerData>("GameControllerData");
        UpdateHealthInterface(l_GameControllerData.m_Health);
        UpdateShieldInterface(l_GameControllerData.m_Shield);
        UpdateBulletInterface(l_GameControllerData.m_CurrentBullets, l_GameControllerData.m_MaxBullets);
        
    }

    public void UpdateWeaponImage()
    {

        m_PistolImage.SetActive(!m_PistolImage.activeInHierarchy);
        m_RifleImage.SetActive(!m_RifleImage.activeInHierarchy);
        
    }

    public void UpdateCrosshair()
    {
        RaycastHit l_raycastHit;
        if (Physics.Raycast(GameController.GetGameController().GetPlayer().m_CurrentWeapon.m_Camera.transform.position, GameController.GetGameController().GetPlayer().m_CurrentWeapon.m_Camera.transform.forward, out l_raycastHit, 100.0f, m_LayerMask.value))
        {
            m_Crosshair.color = m_EnemyCrosshairColor;
            
        }
        else
        {
            m_Crosshair.color = m_CrosshairColor;
        }

    }

    public void SetDieInterface()
    {
        Cursor.visible = true;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float l_CurrentAlpha = 0.0f;
        
        while (m_DieImage.color.a <= 1.0f)
        {
            l_CurrentAlpha += m_AlphaSpeed * Time.deltaTime;
            m_DieImage.color = new Color(m_DieImage.color.r, m_DieImage.color.g, m_DieImage.color.b, l_CurrentAlpha);
            yield return null;
        }
        GameController.GetGameController().RestartGame();
        GameController.GetGameController().GetPlayer().m_CharacterController.enabled = false;
        yield return new WaitForSeconds(4f);
        m_ButtonRetry.SetActive(true);
    }

    IEnumerator FadeOut()
    {
        float l_CurrentAlpha = 1.0f;
        while (m_DieImage.color.a >= 0f)
        {
            l_CurrentAlpha -= m_AlphaSpeed * Time.deltaTime;
            m_DieImage.color = new Color(m_DieImage.color.r, m_DieImage.color.g, m_DieImage.color.b, l_CurrentAlpha);
            yield return null;
        }
       
        
    }

    public void OnRetryClick()
    {
        GameController.GetGameController().GetPlayer().m_CharacterController.enabled = true;
        Cursor.visible = false;
        StartCoroutine(FadeOut());
    }
}
