using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour {

    
    public SelectedObject indicator;
    public bool selected;

    public void Start()
    {
        indicator = Instantiate(GameManager.instance.selectionPrefab, transform.position, Quaternion.identity, transform);
        indicator.gameObject.SetActive(false);
        //indicator.transform.SetParent(gameObject.transform);
    }

    public void Select()
    {
        if (selected)
        {
            selected = false;
            GameManager.instance.selectedEntity = null;
            indicator.gameObject.SetActive(false);
            
        } else
        {
            selected = true;
            indicator.gameObject.SetActive(true);
            GameManager.instance.selectedEntity = GetComponent<Entity>();

        }
    }

    
    private void OnMouseDown()
    {
        Select();
    }
    

}
