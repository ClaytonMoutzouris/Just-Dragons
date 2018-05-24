using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorState { FreeRoam, InCombat, ConfirmTarget };

public class Cursor : MonoBehaviour {
    public static Cursor instance;
    CameraRaycaster cameraRaycaster;
    public IEntity currentPlayer;
    public CursorState cursorState;
    bool Looting = false;
	// Use this for initialization
	void Start () {
        if (instance != null)
        {
            //We are trying to instantiate another cursor, so let it die
            Destroy(this);
        }

        instance = this;
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cursorState = CursorState.FreeRoam;
        
	}

    public Tile SelectedTile()
    {
        if (cameraRaycaster.layerHit != Layer.RaycastEndStop)
        {

            Vector2 TilePos = TileMapManager.Instance.MouseToTilePosition(cameraRaycaster.hit2D.point);
            return TileMapManager.Instance.CurrentMap.GetTile((int)TilePos.x, (int)TilePos.y);
        }
        else
        {
            return null;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (cameraRaycaster.layerHit != Layer.RaycastEndStop)
        {

            Vector2 TilePos = TileMapManager.Instance.MouseToTilePosition(cameraRaycaster.hit2D.point);
            Tile t = TileMapManager.Instance.CurrentMap.GetTile((int)TilePos.x, (int)TilePos.y);

            if (t != null && t.Occupant != null)
            {
                UIManager.Instance.ShowTooltip(t.Occupant);
                TileSelectionIndicator.Instance.SetPosition(new Vector2(-10, -10));
            }
            else
            {
                TileSelectionIndicator.Instance.SetPosition(TilePos);
                UIManager.Instance.HideTooltip();
            }

            

        }
        else
        {
            UIManager.Instance.HideTooltip();
            TileSelectionIndicator.Instance.SetPosition(new Vector2(-10, -10));

        }


    }

}
