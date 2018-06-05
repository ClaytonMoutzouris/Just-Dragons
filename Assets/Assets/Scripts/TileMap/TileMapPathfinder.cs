using Priority_Queue;
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
    }

    public static List<Tile> Path(Tile startTile, Tile goal)
    {
        SimplePriorityQueue<Tile> frontier = new SimplePriorityQueue<Tile>();
        Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();
        Dictionary<Tile, float> costSoFar = new Dictionary<Tile, float>();
        bool reachedGoal = false;
        //if we dont find a path, this will give us the closest we could have got
        Tile altEnd = null;
        float altEndScore = Mathf.Infinity; 

        Tile temp;
        frontier.Enqueue(startTile,0);
        cameFrom.Add(startTile, null);
        costSoFar.Add(startTile, 0);

        while (!(frontier.Count <= 0))
        {
            temp = frontier.Dequeue();
            if(temp == goal)
            {
                reachedGoal = true;
                //then we are there
                break;
            }

            foreach(Tile next in temp.Neighbours)
            {
                if (next == null || next.GetMovementCost() == 0)
                    continue;

                var newCost = costSoFar[temp] + next.GetMovementCost();
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    var priority = newCost + DistanceEstimate(goal, next);
                    if (altEnd == null || DistanceEstimate(goal, next) < altEndScore)
                    {
                        altEnd = next;
                        altEndScore = DistanceEstimate(goal, next);
                    }

                    frontier.Enqueue(next, priority);
                    cameFrom[next] = temp;
                }
            }

        }

        Tile current = null;

        if (reachedGoal)
        {
            current = goal;
        }
        else
        {

            current = altEnd;
        }

        List<Tile> path = new List<Tile>();

        while(current != startTile)
        {
            if (current == null)
                break;

            path.Add(current);
            //this happens if there isnt any paths, so we will break out of the loop

            cameFrom.TryGetValue(current, out current);
            //Might be some error here in the future
        }

        path.Add(startTile);
        path.Reverse();


        return path;
    }

    static float DistanceEstimate(Tile a, Tile b)
    {
        return Mathf.Abs(a.TileX - b.TileX) + Mathf.Abs(a.TileY - b.TileY);
    }

    static float DistanceBetween(Tile a, Tile b)
    {
        //we can make assumptions because we know we're on the grid


        if (Mathf.Abs(a.TileX - b.TileX) + Mathf.Abs(a.TileY - b.TileY) == 1)
        {
            return 1f;
        } // Check hori/vert adjacency
        if ((Mathf.Abs(a.TileX - b.TileX) == 1 && Mathf.Abs(a.TileY - b.TileY) == 1))
        {
            return 1.41421356237f;
        } // Check diag adjacency

        return Mathf.Sqrt(
            Mathf.Pow(a.TileX - b.TileX, 2) +
            Mathf.Pow(a.TileY - b.TileY, 2));

    }
}
