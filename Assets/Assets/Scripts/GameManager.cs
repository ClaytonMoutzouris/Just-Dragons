using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    ITileMapView MapView;
    ITileMapHandler MapHandler;
    ITileMapModel MapModel;
    public List<Character> characters;
    //IEnemyView EnemyViews;
    // Use this for initialization
    public static GameManager instance;

    void Awake () {
        GameManager.instance = this;
        var prefab = Resources.Load<GameObject>("Prefabs/TileMap");
        var instance = Instantiate(prefab);
        MapView = instance.GetComponent<ITileMapView>();

        MapHandler = new TileMapHandler(MapView);

        MapModel = new TileMapModel(50, 50);
        MapHandler.NewMap(MapModel);


        characters = new List<Character>();
        GameObject characterTemp = null;
        prefab = Resources.Load<GameObject>("Prefabs/Character");
        characterTemp = Instantiate(prefab);
        characters.Add(characterTemp.GetComponent<Character>());
        StartGame();

    }

    void StartGame()
    {
        characters[0].SetTile(MapModel.tiles[25, 25]);
        characters[0].transform.SetPositionAndRotation(characters[0].Tile.GetWorldPos(), Quaternion.identity);
        Camera.main.GetComponent<CameraController>().target = characters[0].transform;
    } 

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            //MapHandler.NewMap(MapModel);

        }

    }
	
}
