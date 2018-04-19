using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHasToolTip {
    string GetTooltip();
}

public class Tooltip : MonoBehaviour {
    Text tooltipText;
    //Vector3 offset;
    public void Awake()
    {
        tooltipText = GetComponentInChildren<Text>();
        //offset = new Vector2(TileMapManager.Instance.GetTileOffset(), TileMapManager.Instance.GetTileOffset());
        
    }

    public void SetTooltipText(IEntity IEntity)
    {
        tooltipText.text = IEntity.GetTooltip();
    }

    public void SetPosition(Vector3 position)
    {
        gameObject.transform.SetPositionAndRotation(position, Quaternion.identity);
    }



}
