using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHasToolTip {
    string GetTooltip();
}

public class Tooltip : MonoBehaviour {
    Text tooltipText;
    public void Awake()
    {
        tooltipText = GetComponentInChildren<Text>();
        
        
    }

    public void SetTooltipText(Entity entity)
    {
        tooltipText.text = entity.GetTooltip();
    }

    public void MoveToMouse()
    {
        gameObject.transform.SetPositionAndRotation(Input.mousePosition, Quaternion.identity);
    }



}
