using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{

    static private List<Item> itemsList;
    static private bool isDatabaseLoaded = false;

    static private void ValidateDatabase() // Is list null and/or loaded?
    {
        if (itemsList == null) itemsList = new List<Item>(); // If list is null, create list
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
        Item[] resources = Resources.LoadAll<Item>(@"Items"); // Load all items from the Resources/Items folder
        foreach (Item item in resources)
        {
            if (!itemsList.Contains(item)) // If list doesn't contain item then add it 
            {
                itemsList.Add(item);
            }
        }
    }

    static public void ClearDatabase() // Clear database to free up memory
    {
        isDatabaseLoaded = false;
        itemsList.Clear();
    }

    static public Item GetItem(int id)
    {
        ValidateDatabase();
        foreach (Item item in itemsList)
        {
            if (item.itemID == id)
            {
                return ScriptableObject.Instantiate(item) as Item;
            }
        }
        return null;
    }

    static public Item GetRandomItem()
    {
        ValidateDatabase();
        Item item = itemsList[Random.Range(0, itemsList.Count)];
        return ScriptableObject.Instantiate(item) as Item;
    }

}