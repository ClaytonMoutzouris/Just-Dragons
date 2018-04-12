using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health {
    public int MaxHealth;
    public int currentHealth;
    public bool isDead = false;
    public Entity entity;

    public Health(Entity entity)
    {
        this.entity = entity;
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
        //if (GetComponent<PlayerCharacter>() != null)
         //   UIManager.Instance.UpdatePlayerInfo();

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
       // if (GetComponent<PlayerCharacter>() != null)
        //    UIManager.Instance.UpdatePlayerInfo();

    }

    public void OnDeath()
    {
        if (entity.character.controller != null)
        {
            if (entity.character.controller.combat != null)
            {
                entity.character.controller.combat.RemoveFromCombat(entity);
            }
        }
       // if(GetComponent<ILootable>() != null) 
      //  GetComponent<ILootable>().LootFlag = true;

        entity.Graphics.entity.transform.Rotate(new Vector3(0,0,90));
        //Destroy(gameObject);
    }
}
