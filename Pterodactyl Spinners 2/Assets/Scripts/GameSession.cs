using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This game session class allows us to preserve information in betwen scenes in order keep track of things like
// how many lives are left, or which of the interactable objects have been triggered for example
public class GameSession : MonoBehaviour
{
    [SerializeField] int livesLeft = 3;
    [SerializeField] Image life1;
    [SerializeField] Image life2;
    [SerializeField] Image life3;
    [SerializeField] Image gameOver;
    [SerializeField] Image gameWon;

    private bool berriesThrown = false;
    private bool butterfliesSpooked = false;
    private Image[] allLives;

    // Here we are creating a Singleton of the GameSession class, meaning that there will only ever be one object created
    // of that Type and when another game object requests to create an instance of it, it will instead get the already existing instance if any
    // Otherwise if there isn't an instance of it, a brand new one will be created. Thats how we ensure that the information this object holds will
    // persist between scenes
    private void Awake()
    {
        int gameSessions = FindObjectsOfType<GameSession>().Length;
        if (gameSessions > 1)
        {
            // If an instance already exists, destroy the one that was just created
            Destroy(gameObject);
        }
        else
        {
            // If this is the first instance created, dont destroy it
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        allLives = new Image[] { life1, life2, life3 };
        gameWon.enabled = false;
        gameOver.enabled = false;
    }

    // This is the method that executes a co-routine that handles displaying of the losing scenario
    // and removing lives from the player if they die also adjusting the ingame HUD
    public void TakeLife()
    {
        if (livesLeft > 1)
        {
            allLives[livesLeft - 1].enabled = false;
            livesLeft--;
            StartCoroutine(DeathDelay());
        }
        else
        {
            StartCoroutine(GameEnd(false));
        }
    }

    // This is the method that executes a co-routine with the winning scenario
    public void WinGame()
    {
        StartCoroutine(GameEnd(true));
    }

    // This method is used to force the GameSession to reset itself and all information it had to the default state
    public void ResetSession()
    {
        Destroy(gameObject);
    }

    // The following two methods are used to set and get weather the berries in level 3 were thrown or not
    public void ThrowBerries()
    {
        berriesThrown = true;
    }

    public bool BerriesThrown()
    {
        return berriesThrown;
    }

    // The following two methods are used to set and get weather the butterflies in level 4 are spooked or not
    public void SpookButterflies()
    {
        butterfliesSpooked = true;
    }

    public bool ButterfliesSpooked()
    {
        return butterfliesSpooked;
    }

    // Method that introduces a delay after the player hs just died in orde rto allow for the animation to complete executing
    // before returning the player to the start of the level
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // This co-routine handles wheather the player wins or loses the game and displays the appropriate message
    // before returning them tothe main menu screen
    private IEnumerator GameEnd(bool won)
    {
        if (won)
            gameWon.enabled = true;
        else
            gameOver.enabled = true;

        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
}
