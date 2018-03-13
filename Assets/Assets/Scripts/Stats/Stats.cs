using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStats
{
    void InitializeStats();
    Attribute GetStat(string name);
    Health GetHealth();

}

public class Stats : MonoBehaviour {
    Dictionary<string, Attribute> statList;
    Health health;
    // Use this for initialization


    public static Stats CreateComponent(GameObject where)
    {
        Stats temp = where.AddComponent<Stats>();
        temp.statList = new Dictionary<string, Attribute>();
        temp.health = where.AddComponent<Health>();
        temp.health.Initialise(15);
        temp.InitializeStats();
        return temp;
    }

    public static Stats CreateComponent(GameObject where, Dictionary<string, Attribute> stats)
    {
        Stats temp = where.AddComponent<Stats>();
        temp.statList = new Dictionary<string, Attribute>();
        temp.health = where.AddComponent<Health>();
        temp.health.Initialise(15);
        temp.InitializeStats();
        return temp;
    }


    void Start () {

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
