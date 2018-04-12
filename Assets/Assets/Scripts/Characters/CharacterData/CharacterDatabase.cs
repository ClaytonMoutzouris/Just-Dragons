using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterDatabase {

    public static List<NPCPrototype> charPrototypes;
    static private bool isDatabaseLoaded = false;
    // Use this for initialization
    static private void ValidateDatabase() // Is list null and/or loaded?
    {
        if (charPrototypes == null) charPrototypes = new List<NPCPrototype>(); // If list is null, create list
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
        NPCPrototype[] resources = Resources.LoadAll<NPCPrototype>(@"Character Prototypes"); // Load all items from the Resources/Items folder
        foreach (NPCPrototype cData in resources)
        {
            if (!charPrototypes.Contains(cData)) // If list doesn't contain item then add it 
            {
                charPrototypes.Add(cData);
            }
        }
    }

    static public void ClearDatabase() // Clear database to free up memory
    {
        isDatabaseLoaded = false;
        charPrototypes.Clear();
    }

    static public NPCPrototype GetCharByID(int id)
    {
        ValidateDatabase();
        foreach (NPCPrototype cData in charPrototypes)
        {
            if (cData.charID == id)
            {
                return ScriptableObject.Instantiate(cData) as NPCPrototype;
            }
        }
        return null;
    }

    static public NPCPrototype GetRandomChar()
    {
        ValidateDatabase();
        NPCPrototype cData = charPrototypes[Random.Range(0, charPrototypes.Count)];
        return ScriptableObject.Instantiate(cData) as NPCPrototype;
    }

    static public Entity CreateCharacter(NPCPrototype cd)
    {
        Entity entity = null;
        entity = new Entity(new CharacterAIInputComponent(), new NPCMovementComponent());
        entity.character = new NPCCharacterComponent(entity, cd);

        return entity;
    }

}
