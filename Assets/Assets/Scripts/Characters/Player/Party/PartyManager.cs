using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

    List<Entity> partyMembers;

    
    public void AddMember(Entity entity)
    {
        partyMembers.Add(entity);
    }


    public List<Entity> GetParty()
    {
        return partyMembers;
    }
	
}
