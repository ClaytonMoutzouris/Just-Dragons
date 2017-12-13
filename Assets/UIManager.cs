using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    // partyPortraitPanel;
    CurrentPlayerInfoBox cpiBox;
    public static UIManager Instance { get; private set; }
    public Tooltip tooltip;
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
        //tooltip = GetComponentInChildren<Tooltip>();
        //cpiBox.gameObject.SetActive(false);
        HideTooltip();
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    public CurrentPlayerInfoBox getCPIBox()
    {
        return cpiBox;
    }

    public void ShowTooltip(Entity entity)
    {
        tooltip.gameObject.SetActive(true);
        tooltip.SetTooltipText(entity);
        tooltip.MoveToMouse();
        

    }

    public void HideTooltip()
    {

        tooltip.gameObject.SetActive(false);

    }
}
