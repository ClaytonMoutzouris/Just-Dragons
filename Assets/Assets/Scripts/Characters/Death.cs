using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    Entity entity;

    public Death CreateComponent(GameObject where)
    {
        Death temp = where.AddComponent<Death>();
        temp.entity = temp.GetComponent<Entity>();

        return temp;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
