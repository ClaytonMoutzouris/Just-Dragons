using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {
    Dictionary<string, Attribute> statList;
    Health health;
	// Use this for initialization
	void Start () {
        statList = new Dictionary<string, Attribute>();
        health = gameObject.AddComponent<Health>();
        health.Initialise(1000);
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
    }

    public Health getHealth()
    {
        return health;
    }

}
