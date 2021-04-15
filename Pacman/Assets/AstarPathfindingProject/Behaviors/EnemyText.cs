using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pathfinding
{
    public class EnemyText : MonoBehaviour
    {
        int enemyScore = 200;
        [SerializeField] Text enemyScoreText;
        int enemiesEaten;
        public GameObject enemyText;
        Animator enemyAnimator;

        // Start is called before the first frame update
        void Start()
        {   
            enemyText = GameObject.Find("Enemy Score Text");
            enemyAnimator = FindObjectOfType<Enemy>().GetComponent<Animator>();
            enemyText.SetActive(false);
            enemyScoreText.text = enemyScore.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (FindObjectOfType<Enemy>().isEaten == true)
            {
                StartCoroutine(FreezeGame());
            }
        }

        IEnumerator FreezeGame()
        {
            enemyAnimator.SetBool("isEaten", false);
            enemyText.SetActive(true);
            Time.timeScale = 0;
            enemiesEaten = FindObjectOfType<Enemy>().enemiesEaten;
            enemyScore *= enemiesEaten;
            enemyScoreText.text = enemyScore.ToString();
            yield return new WaitForSecondsRealtime(1);
            Time.timeScale = 1;
            enemyAnimator.SetBool("isEaten", true);
            enemyText.SetActive(false);
        }
    }
}

