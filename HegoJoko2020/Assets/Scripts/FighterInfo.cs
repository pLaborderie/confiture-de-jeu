using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterInfo : MonoBehaviour
{
    public float f_health;
	public Slider healthBar;

    void Start()
    {

    }

    void Update()
    {
        healthBar.value = f_health;
    }

    public void TakeDamage(float damageDealt)
	{
		f_health -= damageDealt;
		Debug.Log("Health = " + f_health.ToString());
	}
}
