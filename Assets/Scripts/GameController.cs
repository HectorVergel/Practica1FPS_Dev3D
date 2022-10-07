using UnityEngine;

public class GameController : MonoBehaviour
{
    float m_PlayerLife = 1.0f;
    public static GameController m_GameController = null;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject); 
       
    }

    public static GameController GetGameController()
    {
        if(m_GameController == null)
        {
            m_GameController = new GameObject("GameController").AddComponent<GameController>();
        }
        return m_GameController;
    }

    public static void DestroySingleton()
    {
        if(m_GameController != null)
        {
            GameObject.Destroy(m_GameController.gameObject);
        }
        m_GameController = null;
    }

    public void SetPlayerLife(float playerLife)
    {
        m_PlayerLife = playerLife;
    }


    public float GetPlayerLife()
    {
        return m_PlayerLife;
    }
}
