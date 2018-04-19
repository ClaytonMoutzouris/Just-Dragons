using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPlayerInfoBox : MonoBehaviour {

    //Parts of the info box
    public ActionBar skillBar;
    public Image background, playerPortrait;
    public HealthBarScript healthBar;
    public ActionPointBar actioBar;
    public IEntity currentPlayer;

    // Use this for initialization
    void Awake () {
        // List<Image> images = new List<Image>();
        //GetComponents(images);
       // healthBar.setHealth();

	}
	
	// Update is called once per frame
	public void UpdateSkills () {
        //healthBar.setHealth(currentPlayer.GetComponent<Stats>().GetHealth());
	}

    public void UpdateHealth()
    {

        healthBar.setHealth(((Character)currentPlayer).Stats.GetHealth());
    }

    public void SetPlayer(IEntity p)
    {
        currentPlayer = p;
        playerPortrait.sprite = currentPlayer.Graphics.sRenderer.sprite;
        UpdateHealth();
        UpdateSkills();
    }
}
