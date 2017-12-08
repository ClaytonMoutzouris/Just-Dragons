using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(ScrollRect))]

public class SkillScrollScript : MonoBehaviour {
    public ScrollRect scrollobject;
    //public GridLayoutGroup grid;
    // Use this for initialization
    void Start () {
        //grid = GetComponent()
        scrollobject = GetComponent<ScrollRect>();
        //scrollobject.scrollSensitivity = grid.spacing.y + grid.cellSize.y;
    }

    // Update is called once per frame
    void Update() {


    }

}
