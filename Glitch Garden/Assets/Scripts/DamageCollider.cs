using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [SerializeField] int amount = 5;
    private void OnTriggerEnter2D()
    {
        FindObjectOfType<HealthPoints>().DecreaseHP(amount);
    }
}
