using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    // This would be our variable that sets how long we have to wait before the next level is loaded
    [SerializeField] float LevelLoadDelay = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Here we execute the co-routine when the player collides with the trigger of the object
        // that denotes the end of the current level
        StartCoroutine(LoadNextLevel());
    }

    // A co-routine that introduces a delay between the player reaching the end of a level and the next one being loaded
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}