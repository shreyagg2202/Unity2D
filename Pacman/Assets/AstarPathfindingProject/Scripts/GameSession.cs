using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pathfinding
{
    public class GameSession : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] int playerLives = 5;
        [SerializeField] int score;
        [SerializeField] Text scoreText;
        [SerializeField] GameObject life1, life2, life3, life4, life5;

        [SerializeField] float timeToWait = 0.5f;
        int currentSceneIndex;

        public void Awake()
        {
            int numGameSessions = FindObjectsOfType<GameSession>().Length;                      // Making GameSession gameobject a singleton
            if (numGameSessions > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
            Instantiate(GameObject.Find("Pickups"));                                            // Instatiating the pickups at the start of every Level
        }

        // start is called before the first frame update
        public void Start()
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            scoreText.text = score.ToString();
        }

        public void Update()
        {
            if (FindObjectsOfType<PacDotsPickup>().Length == 0)                                   // When all the pickups are destroyed, load next level
            {
                LoadNextLevel();
            }
            //Debug.Log(FindObjectOfType<Enemy>().enemiesEaten);
            NumberOfLivesLeft();
        }

        public void AddScore(int pointsToAdd)                                                      // Add Score and update the score
        {
            score += pointsToAdd;
            scoreText.text = score.ToString();
        }

        public void PacmanDeath()                                                                   // if lives > 1, reduce one life
        {
            if (playerLives >= 1)
            {
                TakeLife();
            }
        }

        private void TakeLife()                                                                     // Take one life everytime pacman dies and reload scene
        {
            playerLives -= 1;
            StartCoroutine(ReloadScene());
        }

        IEnumerator ReloadScene()                                                                   // Reload the current scene
        {
            yield return new WaitForSecondsRealtime(timeToWait);
            if (playerLives >= 1)
            {
                SceneManager.LoadScene(currentSceneIndex);
            }

            else
            {
                ResetGameSession();
            }
        }

        public void LoadNextLevel()                                                                 // Load Next Level
        {
            SceneManager.LoadScene(currentSceneIndex += 1);
            Destroy(GameObject.Find("Pickups"));
        }
        public void ResetGameSession()                                                              // When no lives left, Load the first Scene
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }

        public void NumberOfLivesLeft()                                                             // Handles the number of lives shown in the canvas
        {
            switch (playerLives)
            {
                case 5:
                    life1.SetActive(true);
                    life2.SetActive(true);
                    life3.SetActive(true);
                    life4.SetActive(true);
                    life5.SetActive(true);
                    break;
                case 4:
                    life1.SetActive(true);
                    life2.SetActive(true);
                    life3.SetActive(true);
                    life4.SetActive(true);
                    life5.SetActive(false);
                    break;
                case 3:
                    life1.SetActive(true);
                    life2.SetActive(true);
                    life3.SetActive(true);
                    life4.SetActive(false);
                    life5.SetActive(false);
                    break;
                case 2:
                    life1.SetActive(true);
                    life2.SetActive(true);
                    life3.SetActive(false);
                    life4.SetActive(false);
                    life5.SetActive(false);
                    break;
                case 1:
                    life1.SetActive(true);
                    life2.SetActive(false);
                    life3.SetActive(false);
                    life4.SetActive(false);
                    life5.SetActive(false);
                    break;
                case 0:
                    life1.SetActive(false);
                    life2.SetActive(false);
                    life3.SetActive(false);
                    life4.SetActive(false);
                    life5.SetActive(false);
                    break;
            }
        }
    }
}
