using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] float timeToWait = 0.5f;

    public void Awake()
    {
        int numameSessions = FindObjectsOfType<GameSession>().Length;
        if (numameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void PacmanDeath()
    {
        if (playerLives >= 1)
        {
            TakeLife();
        }

        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(timeToWait);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
