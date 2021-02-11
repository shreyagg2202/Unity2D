using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] float baseLives = 3;
    [SerializeField] int damage = 1;
    float healthPoints;
    Text healthPointsText;

    // Start is called before the first frame update
    void Start()
    {
        healthPoints = baseLives - PlayerPrefsController.GetDifficulty();
        healthPointsText = GetComponent<Text>();
        UpdateDisplay();
        Debug.Log("difficulty setting currently is " + PlayerPrefsController.GetDifficulty());
    }

    private void UpdateDisplay()
    {
        healthPointsText.text = healthPoints.ToString();
    }

    public void DecreaseHP(int amount)
    {
        healthPoints -= amount;
        UpdateDisplay();

        if (healthPoints <= 0)
        {
            FindObjectOfType<LevelController>().HandleLoseCondition();
        }
    }
}
