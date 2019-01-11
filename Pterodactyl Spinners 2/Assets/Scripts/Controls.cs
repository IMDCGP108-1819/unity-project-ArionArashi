using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    // This allows us to go back to the main menu from the Controls screen
    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
