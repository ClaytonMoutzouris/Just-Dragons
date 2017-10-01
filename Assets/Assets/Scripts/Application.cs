using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour {

    ITileMapView View;
    ITileMapHandler Handler;
    // Use this for initialization
    void Awake () {

        var prefab = Resources.Load<GameObject>("Prefabs/TileMap");
        var instance = UnityEngine.Object.Instantiate(prefab);
        View = instance.GetComponent<ITileMapView>();

        Handler = new TileMapHandler(View);
	}

    
	
}
