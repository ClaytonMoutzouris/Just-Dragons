using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemRarity { Junk, Common, Uncommon, Rare, Unique };

[System.Serializable]
public class Item : ScriptableObject {

    [SerializeField]
    public int itemID;
    [SerializeField]
    public string itemName;
    [SerializeField]
    int goldValue;
    [SerializeField]
    ItemRarity rarity;
    [SerializeField]
    public Sprite sprite;

	

}


public class ConsumableItem : Item
{

    public virtual void Use(IEntity user)
    {
        if (!CheckValid(user))
            return;
    }

    public virtual bool CheckValid(IEntity user)
    {
        return true;
    }
}

public class Potion : ConsumableItem
{
    [SerializeField]
    string color;

    public override void Use(IEntity user)
    {
        base.Use(user);
    }

    public override bool CheckValid(IEntity user)
    {
        return true;
    }
}


public class EquipmentItem : Item
{

    public virtual void Equip()
    {

    }

    public virtual void Unequip()
    {

    }
}