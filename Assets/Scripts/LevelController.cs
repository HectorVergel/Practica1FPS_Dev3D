using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Transform m_SpawnCheckPoint;
    Transform m_LevelChekcPoint;

    void Start()
    {
        GameController.GetGameController().SetLevel(this);
        GameController.GetGameController().SetAllItems();
        GameController.GetGameController().SetAllEnemies();

        m_LevelChekcPoint = m_SpawnCheckPoint;
        SetPlayer();
    }
    public Transform GetLastCheckPoint()
    {
        Transform l_newCheckpoint = m_LevelChekcPoint;
        return l_newCheckpoint;
    }

    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void SetNewCheckPoint(Transform checkpoint)
    {
        m_LevelChekcPoint = checkpoint;
    }

   

    void SetPlayer()
    {
        GameController.GetGameController().GetPlayer().m_CharacterController.enabled = false;
        GameController.GetGameController().GetPlayer().transform.position = m_LevelChekcPoint.position;
        GameController.GetGameController().GetPlayer().m_CharacterController.enabled = true;

    }
}
