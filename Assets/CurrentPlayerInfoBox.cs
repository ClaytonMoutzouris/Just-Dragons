using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPlayerInfoBox : MonoBehaviour {

    //Parts of the info box
    public SkillBar skillBar;
    public Image background, playerPortrait;
    public HealthBarScript healthBar;
    public ActionPointBar actioBar;
    public Player currentPlayer;

    // Use this for initialization
    void Awake () {
        // List<Image> images = new List<Image>();
        //GetComponents(images);
       // healthBar.setHealth();

	}
	
	// Update is called once per frame
	public void UpdateCurrentPlayer (Player p) {
        currentPlayer = p;
        healthBar.setHealth(currentPlayer.GetComponent<Stats>().GetHealth());
	}
}
