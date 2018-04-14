using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLog : MonoBehaviour {
    public static TextLog instance;
    public GameObject container;
    public Text prefab;
    public List<Text> textLog;
	// Use this for initialization
	void Start () {
        instance = this;
        textLog = new List<Text>();
	}
	
    public void AddEntry(string formattedString)
    {
        Text temp = Instantiate(prefab, container.transform);
        temp.text = formattedString;
    }

}
