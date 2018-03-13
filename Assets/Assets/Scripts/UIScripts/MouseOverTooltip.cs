using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseOverTooltip : MonoBehaviour
{
    Entity entity;

    private void Start()
    {
        entity = GetComponent<Entity>();
    }

    void OnMouseOver()
    {
        UIManager.Instance.ShowTooltip(entity);
    }

    void OnMouseExit()
    {
        UIManager.Instance.HideTooltip();

    }
}