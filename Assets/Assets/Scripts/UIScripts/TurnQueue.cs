using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnQueue : MonoBehaviour {
    List<QueueObject> queuedObjects;
    QueueObject prefab;
    public static TurnQueue Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        prefab = Resources.Load<QueueObject>("Prefabs/QueueObject");
        queuedObjects = new List<QueueObject>();
    }
    public void FillQueue(List<Character> characters)
    { 
        for(int i = 0; i < characters.Count; i++)
        {
            
            QueueObject newObject = Instantiate(prefab);
            newObject.transform.SetParent(transform);

            newObject.SetObject(characters[i].Graphics.sRenderer.sprite, Hostility.Friendly);
            queuedObjects.Add(newObject);
        }

    }

    public void EmptyQueue()
    {
        foreach(QueueObject q in queuedObjects)
        {
            Destroy(q.gameObject);
        }
        queuedObjects.Clear();
    }

    public void RemoveAtIndex(int index)
    {
        QueueObject temp = queuedObjects[index];
        queuedObjects.RemoveAt(index);
        Destroy(temp.gameObject);

    }

    public void AddToQueue(IEntity c)
    {
        QueueObject newObject = Instantiate(prefab);
        newObject.transform.SetParent(transform);

        newObject.SetObject(c.Graphics.sRenderer.sprite, Hostility.Friendly);
        queuedObjects.Add(newObject);
    }

    public void UpdateQueue(int index)
    {

        queuedObjects[index].transform.SetAsFirstSibling();

        

        

        

    }
}
