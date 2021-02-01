using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] int healthPoints = 100;
    [SerializeField] int amount = 5;
    Text healthPointsText;

    // Start is called before the first frame update
    void Start()
    {
        healthPointsText = GetComponent<Text>();
        UpdateDisplay();
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
            FindObjectOfType<LevelLoad>().LoadYouLose();
        }
    }
}
