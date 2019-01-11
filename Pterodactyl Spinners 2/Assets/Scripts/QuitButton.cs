using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    // This function handles the player returning to the main menu after they click the Quit icon whilst in game
    public void Quit()
    {
        FindObjectOfType<GameSession>().ResetSession();
        SceneManager.LoadScene("MainMenu");
    }
}
