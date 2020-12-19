using UnityEngine;

public class DealDamage : MonoBehaviour
{

  public void UpdateHealth(int dam)

  {
    PlayerHealth playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    playerStats.TakeDamage(dam);
  }

  public void UpdateHealthS(int dam)
  {
    PlayerHealthS playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthS>();
    playerStats.TakeDamage(dam);
  }
}
