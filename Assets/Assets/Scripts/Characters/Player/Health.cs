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
        
        GetComponent<ITurnHandler>().Combat.RemoveFromCombat(GetComponent<ITurnHandler>());
        GetComponent<ITurnHandler>().DeactivateTurnHandler();
        if(GetComponent<ILootable>() != null) 
        GetComponent<ILootable>().LootFlag = true;
        //Destroy(gameObject);
    }
}
