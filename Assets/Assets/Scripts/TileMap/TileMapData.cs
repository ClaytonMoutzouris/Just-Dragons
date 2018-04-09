
using System.Collections.Generic;
using UnityEngine;




// Interface for the model
public interface ITileMapModel
{
    // Dispatched when a Tiles type has been changed
    void BuildMap();
    void ChangeTile(int x, int y);
    Tile[,] tiles { get; }
    Vector2 mapSize { get; }
    int mapID { get; }
    List<NPCPrototype> Characters { get; set; }
    //List<IEnemyController> enemyList { get; }
}

// Implementation of the enemy model interface
public class TileMapData : ITileMapModel
{
    public Vector2 mapSize { get; set; }
    //public List<IEnemyController> enemyList { get; set; }
    public Tile[,] tiles { get; set; }
    private List<NPCPrototype> characters;
    //List<Exit> exits;
    public int mapID { get; set; }

    public List<NPCPrototype> Characters
    {
        get
        {
            return characters;
        }

        set
        {
            characters = value;
        }
    }

    //a map holds its own pathfinding object, as it will sometimes need updating


    public TileMapData(int xSize, int ySize, int ID)
    {
        characters = new List<NPCPrototype>();
        mapSize = new Vector2(xSize, ySize);
        tiles = new Tile[xSize, ySize];
        mapID = ID;
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[x, y];
    }

    public void ChangeMap()
    {

    }
    
    public void ChangeTile(int x, int y)
    {
        Tile cTile = tiles[x, y];
        if ((int)cTile.TileType < 4)
        {
            cTile.TileType = cTile.TileType + 1;
        }
        else
        {
            cTile.TileType = TileType.Floor;
        }

        // Dispatch the 'on clicked' event
    }

    public virtual void BuildMap()
    {
        int exit_x, exit_y;
        exit_x = (int)UnityEngine.Random.Range(1, mapSize.x - 2);
        exit_y = (int)UnityEngine.Random.Range(1, mapSize.y - 2);
        Debug.Log("Exit at: " + exit_x + ", " + exit_y);

        for (int y = 0; y< mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                
                if(x == 0 || x == mapSize.x - 1 || y == 0 || y == mapSize.y - 1)
                {
                    tiles[x, y] = new Tile(x, y, TileType.Wall);
                } else
                {
                    if (x == exit_x && y == exit_y)
                    {
                        tiles[x, y] = new Tile(x, y, TileType.Exit, new Exit(this, mapID +1, 25 , 25));
                    }
                    else
                    {
                        tiles[x, y] = new Tile(x, y, TileType.Floor);
                    }
                }
                
                //tiles[x, y] = new Tile(x, y, TileType.Floor);

            }
        }
        //exits.Add(new Exit())
    }

    protected void AddEntities()
    {
        for (int i = 0; i < 10; i++)
        {
            Characters.Add(CharacterDatabase.GetRandomChar());

        }
    }



}
