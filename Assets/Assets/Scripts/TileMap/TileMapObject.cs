﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Dispatched when the map is clicked
public class OnMapClickedEventArgs : EventArgs
{
    public int x { get; set; }
    public int y { get; set; }
}

// Interface for the tilemap view
public interface ITileMapObject
{
    // Dispatched when the tilemap is clicked
    event EventHandler<OnMapClickedEventArgs> OnClicked;
    void DrawTile(Tile tile);
    void DrawMap(Tile[,] tiles, int xSize, int ySize);
    Vector3 TileOffset();
}


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMapObject : MonoBehaviour, ITileMapObject {

    const int MAXSIZEX = 100, MAXSIZEY = 100;
    private float tileSize = 1.0f;
    private Vector3 tileOffset;

    Texture2D texture;
    Color[][] tileTextures;
    public int tileResolution;
    private Mesh mesh;
    private Vector3[] vertices;

    public Texture2D terrainTiles;

    public Vector3 TileOffset()
    {
        return tileOffset;
    }

    public event EventHandler<OnMapClickedEventArgs> OnClicked = (sender, e) => { };

    private void OnMouseOver()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Use a raycast to see where on the tilemap the mouse is located;
        Physics.Raycast(ray, out hit);
        //Can refactor some of this so we dont need to repeat so much, will do for now
        Vector2 tilePos = new Vector3(Mathf.FloorToInt(hit.point.x / tileSize) + TileOffset().x, Mathf.FloorToInt(hit.point.y / tileSize) + TileOffset().y);
        TileSelectionIndicator.Instance.transform.SetPositionAndRotation(tilePos, Quaternion.identity);
        
        /*
        if (Input.GetMouseButtonDown(0))
        {
            var eventArgs = new OnMapClickedEventArgs();
            eventArgs.x = Mathf.FloorToInt(hit.point.x / tileSize);
            eventArgs.y = Mathf.FloorToInt(hit.point.y / tileSize);
            OnClicked(this, eventArgs);
        }
        */
    }

    void Update()
    {


        /*
        // If the primary mouse button was pressed this frame
        if (Input.GetMouseButtonDown(0))
        {
            // If the mouse hit the map
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                Vector2 tilePos = new Vector3(Mathf.FloorToInt(hit.point.x / tileSize) + TileOffset(), Mathf.FloorToInt(hit.point.y / tileSize) + TileOffset());
                TileSelectionIndicator.Instance.transform.SetPositionAndRotation(tilePos, Quaternion.identity);

                // Dispatch the 'on clicked' event
                var eventArgs = new OnClickedEventArgs();
                eventArgs.x = Mathf.FloorToInt(hit.point.x / tileSize);
                eventArgs.y = Mathf.FloorToInt(hit.point.y / tileSize);
                OnClicked(this, eventArgs);
            }
        }
        */
    }



    public void Awake()
    {
        ChopUpTiles();
        GenerateMesh();
        BuildInitialTexture();
        tileOffset = new Vector3(tileSize / 2, tileSize / 2, 0);
    }



    public void DrawMap(Tile[,] tiles, int xSize, int ySize)
    {
        Color[] p;
        for (int y = 0; y < MAXSIZEY; y++)
        {
            for (int x = 0; x < MAXSIZEY; x++)
            {
                if (x < xSize && y < ySize)
                {
                     p = tileTextures[(int)tiles[x, y].TileType];
                    
                } else
                {
                     p = tileTextures[(int)TileType.Blank];
                }
                
                texture.SetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution, p);

            }
        }

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();

        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        mesh_renderer.sharedMaterials[0].mainTexture = texture;


    }

    void ChopUpTiles()
    {
        int numTilesPerRow = terrainTiles.width / tileResolution;
        int numRows = terrainTiles.height / tileResolution;

        Color[][] tiles = new Color[numTilesPerRow * numRows][];

        for(int y = 0; y<numRows; y++)
        {
            for (int x = 0; x < numTilesPerRow; x++)
            {
                tiles[y*numTilesPerRow + x] = terrainTiles.GetPixels(x*tileResolution, y*tileResolution, tileResolution, tileResolution);
            }
        }
        tileTextures = tiles;

    }

    public void DrawTile(Tile tile)
    {
        texture.SetPixels(tile.TileX * tileResolution, tile.TileY * tileResolution, tileResolution, tileResolution, tileTextures[(int)tile.TileType]);
        texture.Apply();
    }

    public void RedrawTile(int x, int y)
    {
        Color[] p = tileTextures[UnityEngine.Random.Range(0, 4)];
        texture.SetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution, p);
        texture.Apply();
    }

    public void DrawMultiTiles(List<Tile> tiles)
    {

        foreach (Tile tile in tiles)
        {
            texture.SetPixels(tile.TileX * tileResolution, tile.TileY * tileResolution, tileResolution, tileResolution, tileTextures[(int)tile.TileType]);
        }
        texture.Apply();
    }

    void BuildInitialTexture()
    {

        int texWidth = MAXSIZEX * tileResolution;
        int texHeight = MAXSIZEY * tileResolution;

        texture = new Texture2D(texWidth, texHeight);
       

        for (int y = 0; y < MAXSIZEY; y++)
        {
            for (int x = 0; x < MAXSIZEX; x++)
            {
                Color[] p = tileTextures[0];
                texture.SetPixels(x*tileResolution, y*tileResolution, tileResolution, tileResolution, p);
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();

        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        mesh_renderer.sharedMaterials[0].mainTexture = texture;

        
    }

    private void GenerateMesh()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        vertices = new Vector3[(MAXSIZEX + 1) * (MAXSIZEY + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= MAXSIZEY; y++)
        {
            for (int x = 0; x <= MAXSIZEX; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / MAXSIZEX, (float)y / MAXSIZEY);
                tangents[i] = tangent;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;

        int[] triangles = new int[MAXSIZEX * MAXSIZEY * 6];
        for (int ti = 0, vi = 0, y = 0; y < MAXSIZEY; y++, vi++)
        {
            for (int x = 0; x < MAXSIZEX; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + MAXSIZEX + 1;
                triangles[ti + 5] = vi + MAXSIZEX + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        MeshCollider mesh_collider = GetComponent<MeshCollider>();
        mesh_collider.sharedMesh = mesh;

    }


}



