using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Health {

    [SerializeField]
    public int MaxHealth;
    private int currentHealth;
    private bool isDead = false;
    public Character entity;

    public bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    public Health()
    {

    }

    public void Initialise(Character entity)
    {
        this.entity = entity;
        CurrentHealth = MaxHealth;
    }

    public void Initialise(int startingHealth)
    {
        MaxHealth = startingHealth;
        CurrentHealth = MaxHealth;
    }

    public void GainLife(int healing)
    {
        CurrentHealth += healing;

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        //update the player info in the UI
        //if (GetComponent<PlayerCharacter>() != null)
         //   UIManager.Instance.UpdatePlayerInfo();

    }

    public bool IsHealthMax()
    {
        return (CurrentHealth == MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {


            CurrentHealth = 0;
            IsDead = true;
            OnDeath();
        }
       // if (GetComponent<PlayerCharacter>() != null)
        //    UIManager.Instance.UpdatePlayerInfo();

    }

    
    public void OnDeath()
    {
        if (entity.controller != null)
        {
            if (entity.controller.combat != null)
            {
                entity.controller.combat.RemoveFromCombat(entity);
            }
        }
        // if(GetComponent<ILootable>() != null) 
        //  GetComponent<ILootable>().LootFlag = true;

        entity.Graphics.entity.transform.Rotate(new Vector3(0,0,90));
        //Destroy(gameObject);
    }
    
}
