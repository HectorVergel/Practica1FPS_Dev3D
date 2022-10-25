using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    float m_PlayerLife;
    int m_PlayerCurrentBullets;
    int m_PlayerMaxBullets;
    float m_PlayerShield;
    FPSPlayerController m_Player;
    InterfaceManager m_Interface;
    ILevelManager m_LevelManager;
    public static GameController m_GameController = null;
    Item[] m_Items;
    IEnemy[] m_Enemies;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SetAllItems();
        SetAllEnemies();
    }

    public static GameController GetGameController()
    {
        if(m_GameController == null)
        {
            m_GameController = new GameObject("GameController").AddComponent<GameController>();
            GameControllerData l_GameControllerData = Resources.Load<GameControllerData>("GameControllerData");
            m_GameController.m_PlayerLife = l_GameControllerData.m_Health;
            m_GameController.m_PlayerShield = l_GameControllerData.m_Shield;
            m_GameController.m_PlayerCurrentBullets = l_GameControllerData.m_CurrentBullets;
            m_GameController.m_PlayerMaxBullets = l_GameControllerData.m_MaxBullets;
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

    public void SetPlayerMaxBullets(int maxBullets)
    {
        m_PlayerMaxBullets = maxBullets;
    }

    public int GetPlayerMaxBullets()
    {
        return m_PlayerMaxBullets;
    }

    public void SetPlayerCurrentBullets(int currentBullets)
    {
        m_PlayerCurrentBullets = currentBullets;
    }

    public int GetPlayerCurrentBullets()
    {
        return m_PlayerCurrentBullets;
    }

    public void SetPlayerShield(float shield)
    {
        m_PlayerShield = shield;
    }

    public float GetPlayerShield()
    {
        return m_PlayerShield;
    }


    public FPSPlayerController GetPlayer()
    {
        return m_Player;
    }

    public void SetPlayer(FPSPlayerController _player)
    {
        m_Player = _player;
    }

    public ILevelManager GetLevel()
    {
        return m_LevelManager;
    }

    public void SetLevel(ILevelManager level)
    {
        m_LevelManager = level;
    }

    public void SetInterface(InterfaceManager _interfacePlayer)
    {
        m_Interface = _interfacePlayer;
    }

    public InterfaceManager GetInterface()
    {
        return m_Interface;
    }
    public void RestartGame()
    {
        m_Player.RestartGame();
        m_Interface.RestartGame();
        RestartItems();
        RestartEnemies();
        
        //Añadir enemigos etc
    }

    void SetAllItems()
    {
        m_Items = FindObjectsOfType<Item>();

    }

    void RestartItems()
    {
        foreach(Item item in m_Items)
        {
            item.RestartGame();
        }
    }

    void SetAllEnemies()
    {
        m_Enemies = FindObjectsOfType<IEnemy>();
    }

    void RestartEnemies()
    {
        foreach(IEnemy enemy in m_Enemies)
        {
            enemy.RestartGame();
        }
    }
}
