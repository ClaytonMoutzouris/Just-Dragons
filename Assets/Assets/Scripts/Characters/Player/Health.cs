using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int MaxHealth;
    public int currentHealth;
    public bool isDead = false;

    public void Start()
    {
        
    }

    public void Initialise(int startingHealth)
    {
        MaxHealth = startingHealth;
        currentHealth = MaxHealth;
    }

    public void GainLife(int healing)
    {
        currentHealth += healing;

        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }

        //update the player info in the UI
        if (GetComponent<PlayerCharacter>() != null)
            UIManager.Instance.UpdatePlayerInfo();

    }

    public bool IsHealthMax()
    {
        return (currentHealth == MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {


            currentHealth = 0;
            isDead = true;
            OnDeath();
        }
        if (GetComponent<PlayerCharacter>() != null)
            UIManager.Instance.UpdatePlayerInfo();

    }

    public void OnDeath()
    {
        if (GetComponent<ITurnHandler>() != null)
        {
            GetComponent<ITurnHandler>().Combat.RemoveFromCombat(GetComponent<ITurnHandler>());

            GetComponent<ITurnHandler>().DeactivateTurnHandler();
        }
        if(GetComponent<ILootable>() != null) 
        GetComponent<ILootable>().LootFlag = true;
        //Destroy(gameObject);
    }
}
