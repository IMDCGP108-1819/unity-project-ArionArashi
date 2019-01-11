using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Simple logic for loading the first game level based on the index it has in the stack of levels
    public void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    // Simple logic for loading the Controls screen
    public void OpenControls()
    {
        SceneManager.LoadScene("Controls");
    }

    // Simple logic for quitting the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
