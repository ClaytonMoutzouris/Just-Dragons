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

    public void NewCombat(List<Character> entities)
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

    public void CheckForCombat(Character e)
    {


        List<Character> temp = new List<Character>();
        foreach(Tile t in TileMapManager.Instance.GetTilesInRange(e.CurrentTile, 5))
        {
            if(t.Occupant != null && t.Occupant != e && (t.Occupant is Character && !((Character)t.Occupant).Stats.GetHealth().IsDead) && !temp.Contains((Character)t.Occupant))
            {
                temp.Add((Character)t.Occupant);
            }
        }



        if(temp.Count > 0)
        {
            if (((Character)e).controller.combat != null)
            {
                foreach(Character it in temp)
                {
                    if(it.controller.combat == null)
                    ((Character)e).controller.combat.JoinCombat(it);
                }
            } else
            {
                temp.Add(e);

                NewCombat(temp);

            }

        }

    }
}
