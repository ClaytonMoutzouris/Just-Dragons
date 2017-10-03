using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour {

    ITileMapView View;
    ITileMapHandler Handler;
    ITileMapModel Model;
    // Use this for initialization
    void Awake () {

        var prefab = Resources.Load<GameObject>("Prefabs/TileMap");
        var instance = Instantiate(prefab);
        View = instance.GetComponent<ITileMapView>();

        

        Handler = new TileMapHandler(View);

        Model = new TileMapModel(50, 50);
        Handler.NewMap(Model);
        
	}

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            Model = new TileMapModel(50, 50);
            Handler.NewMap(Model);
        }

    }




    
	
}
