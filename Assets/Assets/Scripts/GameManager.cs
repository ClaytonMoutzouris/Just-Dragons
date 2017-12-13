using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    ITileMapObject MapView;
    TileMapManager MapManager;
    public List<Entity> characters;
    int currentCharacterIndex;
    //IEnemyView EnemyViews;
    // Use this for initialization
    public static GameManager instance;

    void Awake () {
        GameManager.instance = this;
        //Load the map prefab and initialize it
        var prefab = Resources.Load<GameObject>("Prefabs/TileMap");
        var instance = Instantiate(prefab);
        MapView = instance.GetComponent<ITileMapObject>();

        //create the map manager and pass it the map object
        MapManager = gameObject.AddComponent<TileMapManager>();
        MapManager.Initialize(MapView);

        //Initialize the starting state of the game
        StartGame();

    }

    void StartGame()
    {
        //create a list of maps, this is for testing purposes at the moment
        //MapHandler.NewMap(MapModel);
        List<ITileMapModel> models = new List<ITileMapModel>();
        models.Add(new TileMapModel(50, 50, models.Count));
        models.Add(new TileMapModel(50, 50, models.Count));
        models.Add(new TileMapModel(50, 50, models.Count));
        models.Add(new TileMapModel(50, 50, models.Count));
        models.Add(new TileMapModel(50, 50, models.Count));


        //give the map manager the list of maps we've made
        MapManager.SetUpTestWorld(models);

        characters = new List<Entity>();
        GameObject characterTemp = null;
        //create the player
        var prefab = Resources.Load<GameObject>("Prefabs/Entity");
        characterTemp = Instantiate(prefab);
        characterTemp.AddComponent<Player>();

        //create the enemies
        characters.Add(characterTemp.GetComponent<Entity>());
        for (int i = 0; i < 2; i++)
        {
            characterTemp = Instantiate(prefab);
            characterTemp.AddComponent<Enemy>();
            characterTemp.GetComponent<Entity>().Name = "Monster " + (i+1);
            characters.Add(characterTemp.GetComponent<Entity>());
        }
        currentCharacterIndex = 0;
        characters[0].GetComponent<ITurnHandler>().SetTurnState(TurnState.Start);

        TurnQueue.Instance.FillQueue(characters.Count);
        //UIManager.Instance.
        //Camera.main.GetComponent<CameraController>().target = characters[0].transform;
    }

    private void Update()
    {

        characters[currentCharacterIndex].GetComponent<ITurnHandler>().HandleTurn();

    }

    

    public void NextTurn()
    {
        currentCharacterIndex++;
        if(currentCharacterIndex >= characters.Count)
        {
            currentCharacterIndex = 0;
        }
        characters[currentCharacterIndex].GetComponent<ITurnHandler>().SetTurnState(TurnState.Start);
        TurnQueue.Instance.UpdateQueue();
    }
	
}
