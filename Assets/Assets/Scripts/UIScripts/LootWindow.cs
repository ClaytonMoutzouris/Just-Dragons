using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootWindow : MonoBehaviour {
    public static LootWindow instance;
    public LootItemObject prefab;
    LootableComponent currentlyLooting;



    private void Start()
    {
        instance = this;
        prefab = Resources.Load<LootItemObject>("Prefabs/LootItemObject");

    }

    void SetLoot(LootableComponent l)
    {
        currentlyLooting = l;
        foreach(Item i in currentlyLooting.Loot)
        {
            LootItemObject temp = Instantiate(prefab, transform.position, Quaternion.identity, transform);
            temp.SetItem(i);
            
        }

    }

    public void ShowLootWindow(LootableComponent l)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        Clear();

       
        SetLoot(l);
       // transform.position = pos;
    }

    public void HideLootWindow()
    {
        Clear();
        gameObject.SetActive(false);
    }

    public void Clear()
    {
        foreach(LootItemObject lootItem in GetComponentsInChildren<LootItemObject>())
        {
            Destroy(lootItem.gameObject);
        }

    }

    public void Close()
    {

    }

}
