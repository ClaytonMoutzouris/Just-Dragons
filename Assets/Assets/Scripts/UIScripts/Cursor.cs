using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorState { FreeRoam, InCombat, ConfirmTarget };

public class Cursor : MonoBehaviour {
    public static Cursor instance;
    CameraRaycaster cameraRaycaster;
    Selectable selected;
    public PlayerCharacter currentPlayer;
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

    void CursorUpdateFreeRoam()
    {

        if (cameraRaycaster.layerHit != Layer.RaycastEndStop)
        {

            Vector2 TilePos = TileMapManager.Instance.MouseToTilePosition(cameraRaycaster.hit2D.point);
            Tile t = TileMapManager.Instance.GetTile((int)TilePos.x, (int)TilePos.y);

            if (t.Occupant != null)
            {
                UIManager.Instance.ShowTooltip(t.Occupant);
            }
            else
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
                        if (Selectable.currentSelected != clicked.GetComponent<Selectable>())
                        {
                            t.Occupant.GetComponent<Selectable>().Select2();
                            LootWindow.instance.HideLootWindow();
                        }
                        else
                        {
                            if (clicked.GetComponent<ILootable>() != null)
                                clicked.GetComponent<ILootable>().OnClick();

                            GameManager.instance.characters[0].GetComponent<PlayerTurnHandler>().SetTarget();
                        }

                    }
                    else // If there is no entity on the tile
                    {
                        if (Selectable.currentSelected != null)
                            Selectable.currentSelected.Deselect();
                        //GameManager.instance.
                        GameManager.instance.characters[0].GetComponent<IMovementController>().MoveToTile(t);
                        LootWindow.instance.HideLootWindow();
                    }
                    //
                }

            }
        }
        else
        {
            UIManager.Instance.HideTooltip();
            TileSelectionIndicator.Instance.SetPosition(new Vector2(-10, -10));

        }
    }

    void CursorUpdateInCombat()
    {

    }

    void CursorUpdateConfirmTarget()
    {
        if (cameraRaycaster.layerHit != Layer.RaycastEndStop)
        {

            Vector2 TilePos = TileMapManager.Instance.MouseToTilePosition(cameraRaycaster.hit2D.point);
            Tile t = TileMapManager.Instance.GetTile((int)TilePos.x, (int)TilePos.y);

            if (t.Occupant != null)
            {
                UIManager.Instance.ShowTooltip(t.Occupant);
            }
            else
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

                        currentPlayer.GetComponent<PlayerTurnHandler>().spellToConfirm.Cast(currentPlayer.GetComponent<Entity>(), new List<Entity>() { clicked });

                        cursorState = CursorState.FreeRoam;

                    }
                    else // If there is no entity on the tile
                    {
                        currentPlayer.GetComponent<PlayerTurnHandler>().spellToConfirm.Deselect(currentPlayer.GetComponent<Entity>());
                        cursorState = CursorState.FreeRoam;
                    }
                }

            }
        }
        else
        {
            UIManager.Instance.HideTooltip();
            TileSelectionIndicator.Instance.SetPosition(new Vector2(-10, -10));

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (cursorState)
        {
            case CursorState.FreeRoam:
                print("Freeroam");
                CursorUpdateFreeRoam();
                break;

            case CursorState.InCombat:
                CursorUpdateInCombat();
                break;

            case CursorState.ConfirmTarget:
                print("Confirm Target");

                CursorUpdateConfirmTarget();
                break;
        }




    }

}
