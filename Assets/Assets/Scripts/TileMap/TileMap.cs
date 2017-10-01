using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Dispatched when the map is clicked
public class OnClickedEventArgs : EventArgs
{
    public int x { get; set; }
    public int y { get; set; }
}

// Interface for the tilemap view
public interface ITileMapView
{
    // Dispatched when the tilemap is clicked
    event EventHandler<OnClickedEventArgs> OnClicked;
    void RedrawTile(int x, int y);

}


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour, ITileMapView {

    public int xSize, ySize;
    public float tileSize = 1.0f;
    Texture2D texture;
    Color[][] tileTextures;
    public int tileResolution;
    private Mesh mesh;
    private Vector3[] vertices;

    public Texture2D terrainTiles;

    public event EventHandler<OnClickedEventArgs> OnClicked = (sender, e) => { };

    void Update()
    {
        // If the primary mouse button was pressed this frame
        if (Input.GetMouseButtonDown(0))
        {
            // If the mouse hit the map
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (
                Physics.Raycast(ray, out hit)
                && hit.transform == transform
            )
            {
                // Dispatch the 'on clicked' event
                var eventArgs = new OnClickedEventArgs();
                eventArgs.x = Mathf.FloorToInt(hit.point.x / tileSize);
                eventArgs.y = Mathf.FloorToInt(hit.point.y / tileSize);
                OnClicked(this, eventArgs);
            }
        }
    }

    public void Awake()
    {
        //ChopUpTiles();
        GenerateMesh();
        BuildTexture();
    }

    Color[][] ChopUpTiles()
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
        return tiles;

    }

    public void RedrawTile(int x, int y)
    {
        Color[] p = tileTextures[UnityEngine.Random.Range(0, 4)];
        texture.SetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution, p);
        texture.Apply();
    }

    void BuildTexture()
    {

        int texWidth = xSize * tileResolution;
        int texHeight = ySize * tileResolution;

        texture = new Texture2D(texWidth, texHeight);
        tileTextures = ChopUpTiles();

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                Color[] p = tileTextures[UnityEngine.Random.Range(0, 4)];
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

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector4[] tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                tangents[i] = tangent;
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.tangents = tangents;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        MeshCollider mesh_collider = GetComponent<MeshCollider>();
        mesh_collider.sharedMesh = mesh;

    }


}



