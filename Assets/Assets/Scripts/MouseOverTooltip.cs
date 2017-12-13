using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseOverTooltip : MonoBehaviour
{
    void OnMouseOver()
    {
        UIManager.Instance.ShowTooltip(GetComponent<Entity>());
    }

    void OnMouseExit()
    {
        UIManager.Instance.HideTooltip();

    }
}