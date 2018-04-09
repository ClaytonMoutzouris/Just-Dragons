using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Hostility { Friendly, Neutral, Hostile };

public class NonPlayerCharacter : Character {

    int sightRange = 10;
    

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



    public static NonPlayerCharacter CreateComponent(GameObject where, NPCPrototype cData)
    {
        NonPlayerCharacter temp = where.AddComponent<NonPlayerCharacter>();
        temp.movement = NPCMovement.CreateComponent(where);
        
        temp.gameObject.AddComponent<NPCTurnHandler>();
        temp.gameObject.AddComponent<Lootable>();
        temp.gameObject.GetComponent<Lootable>().LootFlag = false;
        temp.movement.SetToTile(TileMapManager.Instance.GetTile(Random.Range(1,48), Random.Range(1,48)));
        temp.gameObject.layer = LayerMask.NameToLayer("Characters");
        //temp.Actions = new List<Action>();
        temp.CharData = cData;

        temp.GetComponent<Entity>().Name = cData.name;
        temp.GetComponent<SpriteRenderer>().sprite = cData.image;


        temp.Portrait = cData.image;


        temp.Hostility = cData.hostility;
        temp.stats = Stats.CreateComponent(temp.gameObject);

        //Set the characters actions
        temp.Actions = new List<Skill>();
        temp.Actions.Add(new AttackAction());
        temp.Actions.Add(new GuardAction());
        
        //
        return temp;
    }


    void Update()
    {
        //HandleMovementInput();

    }

}
