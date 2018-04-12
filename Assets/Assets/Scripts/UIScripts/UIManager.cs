using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    // partyPortraitPanel;
    CurrentPlayerInfoBox cpiBox;
    ActionBar actionBar;
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
        actionBar = GetComponentInChildren<ActionBar>();
        //tooltip = GetComponentInChildren<Tooltip>();
        //cpiBox.gameObject.SetActive(false);
        HideTooltip();

        

    }



    public void UpdatePlayerInfo()
    {
        cpiBox.UpdateHealth();
        cpiBox.UpdateSkills();
    }

    public void SetCurrentPlayer(Entity p)
    {
        cpiBox.SetPlayer(p);
        actionBar.SetActions(p.character.actions);
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
        tooltip.SetPosition(Camera.main.WorldToScreenPoint(entity.Graphics.entity.transform.position + TileMapManager.Instance.GetTileOffset()));
        

    }

    public void HideTooltip()
    {

        tooltip.gameObject.SetActive(false);

    }
}
