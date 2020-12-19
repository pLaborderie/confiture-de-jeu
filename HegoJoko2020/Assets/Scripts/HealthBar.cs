using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    PlayerHealth playerHealth;


    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        healthBar.value = playerHealth.health;

    }

    void Update()
    {
        healthBar.value = playerHealth.health;
    }
}



