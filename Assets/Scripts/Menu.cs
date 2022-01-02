using UnityEngine;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.instance.LoadLevel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
