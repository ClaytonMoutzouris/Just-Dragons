using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {
    CameraRaycaster cameraRaycaster;
    Selectable selected;
    bool Looting = false;
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
                    

                    Entity clicked = t.Occupant;

                    //If there is an entity on the Tile
                    if (clicked != null)
                    {
                        if(Selectable.currentSelected != clicked.GetComponent<Selectable>())
                        {
                            t.Occupant.GetComponent<Selectable>().Select2();
                            LootWindow.instance.HideLootWindow();
                        } else
                        {
                            if (clicked.GetComponent<ILootable>() != null)
                                clicked.GetComponent<ILootable>().OnClick();

                            GameManager.instance.characters[0].GetComponent<PlayerTurnHandler>().SetTarget(Selectable.currentSelected.GetComponent<Entity>());
                        }
                        
                    }
                    else // If there is no entity on the tile
                    {
                        if(Selectable.currentSelected != null)
                        Selectable.currentSelected.Deselect();
                        //GameManager.instance.
                        GameManager.instance.characters[0].GetComponent<CharacterMovement>().MoveToTile(t);
                        LootWindow.instance.HideLootWindow();
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
