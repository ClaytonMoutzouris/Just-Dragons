using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QueueObject : MonoBehaviour {

    public Image portrait;
    public Image background;
	// Use this for initialization
	void Start () {
        

    }
	
    public void SetObject(Sprite s, Hostility h)
    {
       // print(portrait);
        switch (h)
        {
            case Hostility.Friendly:
                background.color = Color.green;
                break;
            case Hostility.Neutral:
                background.color = Color.yellow;
                break;
            case Hostility.Hostile:
                background.color = Color.red;
                break;

        }
        portrait.sprite = s;
    }

}
