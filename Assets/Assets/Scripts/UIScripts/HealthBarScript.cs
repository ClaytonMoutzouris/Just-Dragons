using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarScript : MonoBehaviour {
    Health health;
    public Image visHealth;
    public Text healthText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void UpdateHealth() {
        healthText.text = health.CurrentHealth + " / " + health.MaxHealth;
        //visHealth.transform.localScale = new Vector3((float)health.CurrentHealth/(float)health.MaxHealth, 1.0f, 1.0f);
    }

    public void setHealth(Health health) {
        this.health = health;
        UpdateHealth();
    }
}
