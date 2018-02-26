using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int MaxHealth;
    public int currentHealth;

    public void Start()
    {
        if (GetComponent<Player>() != null)
            UIManager.Instance.getCPIBox().UpdateCurrentPlayer(GetComponent<Player>());
    }

    public void Initialise(int startingHealth)
    {
        MaxHealth = startingHealth;
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            currentHealth = 0;

        if (GetComponent<Player>() != null)
            UIManager.Instance.getCPIBox().UpdateCurrentPlayer(GetComponent<Player>());

    }
}
