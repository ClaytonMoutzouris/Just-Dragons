using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LootItemObject : MonoBehaviour {
    Item item;
    public Image image;
    public Text info;

	// Use this for initialization
	void Start () {

        image = GetComponentInChildren<Image>();
        info = GetComponentInChildren<Text>();
       // print(image + " image");
        //print(info + " text");

    }

    public void SetItem(Item i)
    {
        print(i);
        item = i;
        info.text = item.itemName;
        image.sprite = i.sprite;
    }

}
