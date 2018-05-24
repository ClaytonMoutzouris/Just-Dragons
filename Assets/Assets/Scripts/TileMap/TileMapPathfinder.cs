using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNodeEdge<T>
{
    public float cost;
    public TileNode<T> node;
}

public class TileNode<T>
{
    public TileNodeEdge<T>[] edges;

    public T data;

}

public static class TileMapPathfinder {
    public static Dictionary<Tile, TileNode<Tile>> TileNodes;
    static ITileMapModel CurrentMap;
    static Queue<Tile> path;

    public static void CreateNodes()
    {

        //Queue tilenodes;
        //tilenodes.Enqueue
        TileNodes = new Dictionary<Tile, TileNode<Tile>>();

        for (int x = 0; x< CurrentMap.mapSize.x; x++)
        {
            for (int y = 0; y < CurrentMap.mapSize.y; y++)
            {
                Tile t = CurrentMap.GetTile(x, y);

                //if(t.movementCost > 0) {	// Tiles with a move cost of 0 are unwalkable
                TileNode<Tile> n = new TileNode<Tile>
                {
                    data = CurrentMap.GetTile(x, y)
                };

                TileNodes.Add(t, n);


            }

        }

        int edgeCount = 0;

        foreach (Tile t in TileNodes.Keys)
        {
            TileNode<Tile> n = TileNodes[t];

            List<TileNodeEdge<Tile>> edges = new List<TileNodeEdge<Tile>>();

            // Get a list of neighbours for the tile
            Tile[] neighbours = CurrentMap.GetNeighbours(t, true);  // NOTE: Some of the array spots could be null.

            // If neighbour is walkable, create an edge to the relevant node.
            for (int i = 0; i < neighbours.Length; i++)
            {
                if (neighbours[i] != null && neighbours[i].GetMovementCost() > 0 && IsClippingCorner(t, neighbours[i]) == false)
                {
                    // This neighbour exists, is walkable, and doesn't requiring clipping a corner --> so create an edge.

                    TileNodeEdge<Tile> e = new TileNodeEdge<Tile>();
                    e.cost = neighbours[i].GetMovementCost();
                    e.node = TileNodes[neighbours[i]];

                    // Add the edge to our temporary (and growable!) list
                    edges.Add(e);

                    edgeCount++;
                }
            }

            n.edges = edges.ToArray();
        }


    }

    static bool IsClippingCorner(Tile curr, Tile neigh)
    {
        // If the movement from curr to neigh is diagonal (e.g. N-E)
        // Then check to make sure we aren't clipping (e.g. N and E are both walkable)

        int dX = curr.TileX - neigh.TileX;
        int dY = curr.TileY - neigh.TileY;

        if (Mathf.Abs(dX) + Mathf.Abs(dY) == 2)
        {
            // We are diagonal

            if (CurrentMap.GetTile(curr.TileX - dX, curr.TileY).GetMovementCost() == 0)
            {
                // East or West is unwalkable, therefore this would be a clipped movement.
                return true;
            }

            if (CurrentMap.GetTile(curr.TileX, curr.TileY - dY).GetMovementCost() == 0)
            {
                // North or South is unwalkable, therefore this would be a clipped movement.
                return true;
            }

            // If we reach here, we are diagonal, but not clipping
        }

        // If we are here, we are either not clipping, or not diagonal
        return false;
    }

    public static void SetMap(ITileMapModel newMap)
    {
        CurrentMap = newMap;
        //CreateNodes();
        //Debug.Log("Number of TileNodes "+ TileNodes.Count);
    }

    public static List<Tile> Path(Tile startTile, Tile goal)
    {
        Queue<Tile> frontier = new Queue<Tile>();
        Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();

        Tile temp;
        frontier.Enqueue(startTile);
        cameFrom.Add(startTile, null);

        while(!(frontier.Count <= 0))
        {
            temp = frontier.Dequeue();
            if(temp == goal)
            {
                //then we are there
                break;
            }
            foreach(Tile next in temp.Neighbours)
            {
                if (next!= null && !cameFrom.ContainsKey(next))
                {
                    frontier.Enqueue(next);
                    cameFrom.Add(next, temp);
                }
            }
        }

        Tile current = goal;
        List<Tile> path = new List<Tile>();

        while(current != startTile)
        {
            path.Add(current);
            cameFrom.TryGetValue(current, out current);
            //Might be some error here in the future
        }

        path.Add(startTile);
        path.Reverse();


        return path;
    }
}
