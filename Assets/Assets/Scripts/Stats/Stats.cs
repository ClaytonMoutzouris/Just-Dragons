using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Stats
{

    [SerializeField]
    Health health;

    [SerializeField]
    CharacterStat[] stats;

    // Use this for initialization

    public Stats()
    {
        health = new Health();
        health.Initialise(10);
        //New stats get created with these default values
        //Movement is the max value of the stat
        stats = new CharacterStat[(int)StatType.Movement];
        for (int i = 0; i < (int)StatType.Movement; i++)
        {
            stats[i] = new CharacterStat((StatType)i, 10);
        }

    }



    public CharacterStat GetStatByType(StatType type)
    {
        return stats[(int)type];
    }

    public Health GetHealth()
    {
        return health;
    }

}
