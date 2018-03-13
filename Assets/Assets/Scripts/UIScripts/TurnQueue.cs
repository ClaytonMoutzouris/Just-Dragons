using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnQueue : MonoBehaviour {
    List<GameObject> queuedObjects;
    int index;
    public static TurnQueue Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


        queuedObjects = new List<GameObject>();
        index = 0;
    }

    public void FillQueue(int n)
    {
        for(int i = 0; i < n; i++)
        {
            var prefab = Resources.Load<GameObject>("Prefabs/QueueObject");
            GameObject newObject = Instantiate(prefab);
            newObject.transform.SetParent(this.transform);
            newObject.GetComponentInChildren<Image>().color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            queuedObjects.Add(newObject);
        }

    }

    public void UpdateQueue()
    {
        queuedObjects[index].transform.SetAsLastSibling();

        index++;

        if (index >= queuedObjects.Count)     
            index = 0;

        

    }
}
