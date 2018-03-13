using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    List<Item> items;

    public static Inventory CreateComponent(GameObject where)
    {
        Inventory temp = where.AddComponent<Inventory>();


        temp.items = new List<Item>();

        return temp;
       
    }

    public Item GetItem(int index)
    {
        return items[index];
    }

    public void AddItem(Item i)
    {
        items.Add(i);
    }

    public void AddItems(List<Item> i)
    {
        items.AddRange(i);
    }

}
