using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject prepareMenu;

    void Start()
    {
        if (PrepareMenu.isReturningFromGame)
        {
            // player is returning from a game -> go right in the PrepareMenu
            prepareMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
