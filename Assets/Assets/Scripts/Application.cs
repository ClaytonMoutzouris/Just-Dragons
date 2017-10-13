using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour {

    ITileMapView MapView;
    ITileMapHandler MapHandler;
    ITileMapModel MapModel;

    //IEnemyView EnemyViews;
    // Use this for initialization
    void Awake () {

        var prefab = Resources.Load<GameObject>("Prefabs/TileMap");
        var instance = Instantiate(prefab);
        MapView = instance.GetComponent<ITileMapView>();

        MapHandler = new TileMapHandler(MapView);

        MapModel = new TileMapModel(50, 50);
        MapHandler.NewMap(MapModel);


        

	}

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            MapModel = new TileMapModel(Random.Range(25, 80), Random.Range(25, 80));
            MapHandler.NewMap(MapModel);
        }

    }
	
}
