using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    // partyPortraitPanel;
    CurrentPlayerInfoBox cpiBox;
    public static UIManager Instance { get; private set; }
    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //get references to its parts
        cpiBox = GetComponentInChildren<CurrentPlayerInfoBox>();
        //cpiBox.gameObject.SetActive(false);

    }

	
	// Update is called once per frame
	void Update () {
		
	}

    public CurrentPlayerInfoBox getCPIBox()
    {
        return cpiBox;
    }

}
