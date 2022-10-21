using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public interface ILevelManager
{
    void SetNewCheckPoint(Transform checkpoint);

    void LoadNewScene(string sceneName);
    Transform GetLastCheckPoint();
}
public class Level2Controller : MonoBehaviour, ILevelManager
{
    public Transform m_SpawnCheckPoint;
    Stack m_Level2ChekcPoints = new Stack();

    private void Start()
    {
        GameController.GetGameController().SetLevel(this);
        m_Level2ChekcPoints.Push(m_SpawnCheckPoint);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }

    public void SetNewCheckPoint(Transform checkpoint)
    {
        m_Level2ChekcPoints.Push(checkpoint);
    }

    public Transform GetLastCheckPoint()
    {
        Transform l_newCheckpoint = (Transform)m_Level2ChekcPoints.Peek();
        return l_newCheckpoint;
    }

    public void LoadNewScene(string newScene)
    {
        SceneManager.LoadSceneAsync(newScene);
    }
}
