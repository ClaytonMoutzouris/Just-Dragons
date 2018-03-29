using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionDatabase
{

    static private List<Action> actionsList;
    static private bool isDatabaseLoaded = false;

    static private void ValidateDatabase() // Is list null and/or loaded?
    {
        if (actionsList == null) actionsList = new List<Action>(); // If list is null, create list
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
        Action[] resources = Resources.LoadAll<Action>(@"Actions"); // Load all items from the Resources/Items folder
        foreach (Action action in resources)
        {
            if (!actionsList.Contains(action)) // If list doesn't contain item then add it 
            {
                actionsList.Add(action);
            }
        }
    }

    static public void ClearDatabase() // Clear database to free up memory
    {
        isDatabaseLoaded = false;
        actionsList.Clear();
    }

    static public Action GetAction(int id)
    {
        ValidateDatabase();
        foreach (Action action in actionsList)
        {
            if (action.actionID == id)
            {
                return ScriptableObject.Instantiate(action) as Action;
            }
        }
        return null;
    }


}