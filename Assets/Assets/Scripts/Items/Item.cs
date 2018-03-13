using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemRarity { Junk, Common, Uncommon, Rare, Unique };

[System.Serializable]
public class Item : ScriptableObject {

    [SerializeField]
    public string itemName;
    [SerializeField]
    int value;
    [SerializeField]
    ItemRarity rarity;
    [SerializeField]
    public Sprite sprite;

	

}
