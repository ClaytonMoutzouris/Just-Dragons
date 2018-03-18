using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour {
    public static CharacterGenerator instance;

	// Use this for initialization
	void Awake () {
        instance = this;
    }
	

    public Entity CreateCharacter(CharacterData cData)
    {
        Entity characterTemp = null;
        //create the player
        var prefab = Resources.Load<Entity>("Prefabs/Entity") as Entity;

        characterTemp = Instantiate(prefab);
            NonPlayerCharacter.CreateComponent(characterTemp.gameObject, cData.hostility, Stats.CreateComponent(characterTemp.gameObject), cData);

        return characterTemp;

    }

}
