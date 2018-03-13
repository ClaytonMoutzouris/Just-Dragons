using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {

    int sizeX, sizeY;
    int posX, posY; //position denoted from top left? maybe bottom left
    List<Tile> tiles;
	
    public void SetToTiles()
    {
        transform.position = TileMapManager.Instance.GetTile(posX, posY).GetWorldPos();
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                TileMapManager.Instance.GetTile(posX + x, posY + y).Occupant = GetComponent<Entity>();
               // tiles.Add
            }
        }
        
    }

    public void SetStructureParameters(int xp, int yp, int xs, int ys)
    {
        sizeX = xs;
        sizeY = ys;
        posX = xp;
        posY = yp;

        

    }

}
