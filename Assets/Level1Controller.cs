using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Controller : MonoBehaviour
{
    void Start()
    {
        
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
