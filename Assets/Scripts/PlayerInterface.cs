using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    public TextMeshProUGUI m_CurrentBulletInterfaceText;
    public TextMeshProUGUI m_MaxBulletInterfaceText;
    public TextMeshProUGUI m_HealthInterfaceText;
    public TextMeshProUGUI m_ShieldInterfaceText;

    private void OnEnable()
    {
        FPSPlayerController.OnReload += UpdateBulletInterface;
    }

    private void OnDisable()
    {
        FPSPlayerController.OnReload -= UpdateBulletInterface;
    }
    public void UpdateBulletInterface(int _currentBullets, int _maxBullets)
    {
        m_CurrentBulletInterfaceText.text = _currentBullets.ToString();
        m_MaxBulletInterfaceText.text ="/" +  _maxBullets.ToString();
    }

    public void UpdateHealthInterface(int _currentHealth)
    {

    }

    public void UpdateShieldInterface(int _currentShield)
    {

    }
}
