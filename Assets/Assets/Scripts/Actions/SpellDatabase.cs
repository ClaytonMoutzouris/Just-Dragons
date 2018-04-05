using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpellDatabase
{

    static private List<Spell> actionsList;
    static private bool isDatabaseLoaded = false;

    static private void ValidateDatabase() // Is list null and/or loaded?
    {
        if (actionsList == null) actionsList = new List<Spell>(); // If list is null, create list
        if (!isDatabaseLoaded) LoadDatabase(); // If database is not loaded, load database
    }

    static public void LoadDatabase()
    {
        if (isDatabaseLoaded) return;
        isDatabaseLoaded = true;
        LoadDatabaseForce();
    }

    static public void LoadDatabaseForce()
    {
        ValidateDatabase();
        Spell[] resources = Resources.LoadAll<Spell>(@"Skills/Spells"); // Load all items from the Resources/Items folder
        foreach (Spell spell in resources)
        {
            if (!actionsList.Contains(spell)) // If list doesn't contain item then add it 
            {
                actionsList.Add(spell);
            }
        }


    }

    static public void ClearDatabase() // Clear database to free up memory
    {
        isDatabaseLoaded = false;
        actionsList.Clear();
    }

    static public Spell GetAction(int id)
    {
        ValidateDatabase();
        foreach (Spell spell in actionsList)
        {
            if (spell.spellID == id)
            {
                return ScriptableObject.Instantiate(spell) as Spell;
            }
        }
        return null;
    }


}