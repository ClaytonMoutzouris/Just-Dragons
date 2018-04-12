using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stats {
    Dictionary<string, Attribute> statList;
    Health health;
    Entity entity;
    // Use this for initialization

    public Stats(Entity entity)
    {
        this.entity = entity;
        statList = new Dictionary<string, Attribute>();
        health = new Health(entity);
        health.Initialise(15);
        InitializeStats();
    }

	
    void InitializeStats()
    {
        statList.Add("Constitution", new Attribute(10));
        statList.Add("Strength", new Attribute(10));
        statList.Add("Dexterity", new Attribute(10));
        statList.Add("Intelligence", new Attribute(10));
        statList.Add("Charisma", new Attribute(10));
        statList.Add("Wisdom", new Attribute(10));
        statList.Add("Movement", new DependantAttribute(10));
    }

    public Attribute GetStat(string statname)
    {
        return statList[statname];
    }

    public Health GetHealth()
    {
        return health;
    }


}
