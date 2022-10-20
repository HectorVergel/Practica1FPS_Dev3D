using UnityEngine;

public class GameController : MonoBehaviour
{
    float m_PlayerLife;
    FPSPlayerController m_Player;
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
            GameControllerData l_GameControllerData = Resources.Load<GameControllerData>("GameControllerData");
            m_GameController.m_PlayerLife = l_GameControllerData.m_Health;
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

    public FPSPlayerController GetPlayer()
    {
        return m_Player;
    }

    public void SetPlayer(FPSPlayerController _player)
    {
        m_Player = _player;
    }

    public void RestartGame()
    {
        m_Player.RestartGame();
        //Añadir enemigos etc
    }
}
