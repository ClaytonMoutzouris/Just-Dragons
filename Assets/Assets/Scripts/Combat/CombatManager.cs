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

    public void NewCombat(List<ITurnHandler> entities)
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
        
        List<ITurnHandler> temp = new List<ITurnHandler>();
        foreach(Tile t in TileMapManager.Instance.GetTilesInRange(e.GetComponent<IMovementController>().CurrentTile, 5))
        {
            if(t.Occupant != null && t.Occupant != e && t.Occupant.GetComponent<ITurnHandler>() != null && !temp.Contains(t.Occupant.GetComponent<ITurnHandler>()) && t.Occupant.GetComponent<Character>() != null && t.Occupant.GetComponent<Character>().Hostility == Hostility.Hostile)
            {
                temp.Add(t.Occupant.GetComponent<ITurnHandler>());
            }
        }



        if(temp.Count > 0)
        {
            if (e.GetComponent<ITurnHandler>().Combat != null)
            {
                foreach(ITurnHandler it in temp)
                {
                    e.GetComponent<ITurnHandler>().Combat.JoinCombat(it);
                }
            } else
            {
                temp.Add(e.GetComponent<ITurnHandler>());

                NewCombat(temp);

            }

        }

    }
}
