using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pathfinding
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] int playerLives = 5;
        [SerializeField] float timeToWait = 0.5f;
        [SerializeField] int score = 0;

        [SerializeField] Text scoreText;
        [SerializeField] GameObject life1, life2, life3, life4, life5;

        public void Awake()
        {
            int numGameSessions = FindObjectsOfType<GameSession>().Length;
            if (numGameSessions > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }

        }
        // Start is called before the first frame update
        public void Start()
        {
            scoreText.text = score.ToString();
        }

        public void Update()
        {
            if (FindObjectOfType<AIDestinationSetter>().frightenedEndTime == 0)
            {
                FindObjectOfType<Enemy>().enemiesEaten = 0;
            }
            NumberOfLivesLeft();
        }

        public void AddScore(int pointsToAdd)
        {
            score += pointsToAdd;
            scoreText.text = score.ToString();
        }

        public void PacmanDeath()
        {
            if (playerLives >= 1)
            {
                TakeLife();
            }
        }

        private void TakeLife()
        {
            playerLives -= 1;
            StartCoroutine(ReloadScene());
        }

        IEnumerator ReloadScene()
        {
            yield return new WaitForSecondsRealtime(timeToWait);
            if (playerLives >= 1)
            {
                var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
            }

            else
            {
                ResetGameSession();
            }
        }

        public void ResetGameSession()
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }

        public void NumberOfLivesLeft()
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
