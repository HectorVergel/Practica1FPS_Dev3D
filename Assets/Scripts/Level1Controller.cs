using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Controller : MonoBehaviour, ILevelManager
{
    public Transform m_SpawnCheckPoint;
    Stack m_Level2ChekcPoints = new Stack();

    
    public Transform GetLastCheckPoint()
    {
        Transform l_newCheckpoint = (Transform)m_Level2ChekcPoints.Peek();
        return l_newCheckpoint;
    }

    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void SetNewCheckPoint(Transform checkpoint)
    {
        m_Level2ChekcPoints.Push(checkpoint);
    }

    void Start()
    {
        GameController.GetGameController().SetLevel(this);
        m_Level2ChekcPoints.Push(m_SpawnCheckPoint);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameController.GetGameController().SetPlayerLife(0.3f);
            SceneManager.LoadSceneAsync("Level2");
        }
    }
}
