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


        StartGame();

    }

    void StartGame()
    {
        //MapModel = new TileMapModel(50, 50, 0);
        //MapHandler.NewMap(MapModel);
        List<ITileMapModel> models = new List<ITileMapModel>();
        models.Add(new TileMapModel(50, 50, models.Count));
        models.Add(new TileMapModel(50, 50, models.Count));
        models.Add(new TileMapModel(50, 50, models.Count));
        models.Add(new TileMapModel(50, 50, models.Count));
        models.Add(new TileMapModel(50, 50, models.Count));

        MapHandler.SetUpTestWorld(models);

        characters = new List<Character>();
        GameObject characterTemp = null;
        var prefab = Resources.Load<GameObject>("Prefabs/Character");
        characterTemp = Instantiate(prefab);
        characters.Add(characterTemp.GetComponent<Character>());
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
