using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

    List<IEntity> partyMembers;

    
    public void AddMember(IEntity IEntity)
    {
        partyMembers.Add(IEntity);
    }


    public List<IEntity> GetParty()
    {
        return partyMembers;
    }
	
    //When the party changes maps, update the current map (draw the new one).
    public void PartyMapChange()
    {

    }


}
