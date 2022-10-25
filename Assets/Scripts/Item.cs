using UnityEngine;

public abstract class Item: MonoBehaviour
{
    public abstract void Update();
    public abstract void Pick(FPSPlayerController Player);
    public void RestartGame()
    {
        this.gameObject.SetActive(true);
    }
}
