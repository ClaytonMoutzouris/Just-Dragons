using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {
    CameraRaycaster cameraRaycaster;
    
	// Use this for initialization
	void Start () {
        cameraRaycaster = GetComponent<CameraRaycaster>();
	}

    // Update is called once per frame
    void Update()
    {
        
        if (cameraRaycaster.layerHit != Layer.RaycastEndStop)
        {
            Vector2 TilePos = TileMapManager.Instance.MouseToTilePosition(cameraRaycaster.hit2D.point);
            Tile t = TileMapManager.Instance.GetTile((int)TilePos.x, (int)TilePos.y);
            if(t.Occupant != null)
            {
                UIManager.Instance.ShowTooltip(t.Occupant);
            } else
            {
                UIManager.Instance.HideTooltip();
            }
            TileSelectionIndicator.Instance.SetPosition(TilePos);

            if (Input.GetMouseButtonDown(0))
            {
                //print(cameraRaycaster.layerHit.ToString());
                if (cameraRaycaster.layerHit != Layer.RaycastEndStop)
                {
                    

                    print(t.TileX + ", " + t.TileY);

                    if (t.Occupant != null)
                    {
                        print(t.Occupant.name);
                        t.Occupant.GetComponent<Selectable>().Select2();
                    }
                    else
                    {
                        //GameManager.instance.
                        print("No Occ");
                    }
                    //
                }

            }
        } else
        {
            UIManager.Instance.HideTooltip();
            TileSelectionIndicator.Instance.SetPosition(new Vector2(-10, -10));

        }


    }

}
