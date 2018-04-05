using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionIconObject : MonoBehaviour, IPointerDownHandler {

    Image image;
    Skill action;
    int index;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}

    public void SetAction(Skill a)
    {
        action = a;
        image.sprite = a.image;
    }

    public void Clear()
    {
        action = null;
        GetComponent<Image>().sprite = null;
    }

    void OnPointerDown()
    {
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(action != null)
        Debug.Log("Clicked! " + action.ActionName);
    }
}
