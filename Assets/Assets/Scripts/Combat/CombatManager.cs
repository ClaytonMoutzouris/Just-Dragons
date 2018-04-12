using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public static CombatManager instance;
    [SerializeField]List<Combat> combats;
    List<Combat> toRemove;

    private void Start()
    {
        combats = new List<Combat>();
        toRemove = new List<Combat>();
        instance = this;
    }

    public void NewCombat(List<Entity> entities)
    {

        Combat temp = new Combat(entities);
        combats.Add(temp);

    }

    void Update()
    {
        if(combats.Count <= 0)
        {
            return;
        }

        foreach(Combat c in combats)
        {

            c.Update();
            if(c.CurrentState == CombatState.End)
            {
                c.EndCombat();
                toRemove.Add(c);
            }
        }

        foreach(Combat tR in toRemove)
        {
            combats.Remove(tR);
        }
       
    }

    //Right now only ever called by the player

    public void CheckForCombat(Entity e)
    {
        
        List<Entity> temp = new List<Entity>();
        foreach(Tile t in TileMapManager.Instance.GetTilesInRange(e.CurrentTile, 5))
        {
            if(t.Occupant != null && t.Occupant != e && !t.Occupant.Stats.GetHealth().isDead && !temp.Contains(t.Occupant) && t.Occupant.character is NPCCharacterComponent)
            {
                temp.Add(t.Occupant);
            }
        }



        if(temp.Count > 0)
        {
            if (e.character.controller.combat != null)
            {
                foreach(Entity it in temp)
                {
                    e.character.controller.combat.JoinCombat(it);
                }
            } else
            {
                temp.Add(e);

                NewCombat(temp);

            }

        }

    }
}
