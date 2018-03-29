using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ILootable
{
    List<Item> Loot { get; set; }
    Entity Entity { get; set; }
    bool LootFlag { get; set; }
    void OnClick();
}

public class Lootable : MonoBehaviour, ILootable {
    List<Item> loot;
    Entity entity;
    bool lootFlag = true;

    public List<Item> Loot
    {
        get
        {
            return loot;
        }

        set
        {
            loot = value;
        }
    }

    public Entity Entity
    {
        get
        {
            return entity;
        }

        set
        {
            entity = value;
        }
    }

    public bool LootFlag
    {
        get
        {
            return lootFlag;
        }

        set
        {
            lootFlag = value;
        }
    }

    // Use this for initialization
    void Start () {
        Entity = GetComponent<Entity>();
        loot = new List<Item>();

        for(int i = 0; i < Random.Range(1,5); i++)
        loot.Add(ItemDatabase.GetRandomItem());
        

    }

    public void OnClick()
    {
        if(LootFlag)
        LootWindow.instance.ShowLootWindow(this);
    }

}
