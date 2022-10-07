using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Controller : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}
