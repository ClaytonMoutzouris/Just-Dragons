using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hostility { Friendly, Neutral, Hostile };

public class NonPlayerCharacter : Character {

    int sightRange = 10;
    Hostility hostility = Hostility.Neutral;
    

    public int SightRange
    {
        get
        {
            return sightRange;
        }

        set
        {
            sightRange = value;
        }
    }

    public Hostility Hostility
    {
        get
        {
            return hostility;
        }

        set
        {
            hostility = value;
        }
    }


    public static NonPlayerCharacter CreateComponent(GameObject where, Hostility hostile, Stats stats)
    {
        NonPlayerCharacter temp = where.AddComponent<NonPlayerCharacter>();
        temp.movement = CharacterMovement.CreateComponent(where);
        
        temp.gameObject.AddComponent<NPCTurnHandler>();
        temp.gameObject.AddComponent<Lootable>();
        temp.gameObject.GetComponent<Lootable>().LootFlag = false;
        temp.movement.SetToTile(TileMapManager.Instance.GetTile(Random.Range(1,48), Random.Range(1,48)));
        temp.gameObject.layer = LayerMask.NameToLayer("Characters");
        //temp.Actions = new List<Action>();

        temp.Hostility = hostile;
        temp.stats = stats;
        print(UIManager.Instance);
        //
        return temp;
    }


    void Update()
    {
        //HandleMovementInput();

    }

}
