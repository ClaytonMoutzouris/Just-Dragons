using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    ITileMapObject MapView;
    TileMapManager MapManager;
    public List<Entity> characters;
    int currentCharacterIndex;
    public Entity selectedEntity = null;
    //IEnemyView EnemyViews;
    // Use this for initialization
    public static GameManager instance;
    public GameObject selectionPrefab;
    public Dictionary<string, Item> itemLibrary;
    public Dictionary<int, Skill> actionDatabase;
    public Entity currentSelected;
    
    


    void Start () {
        //Set the game managers instance to this so that we can access it statically
        GameManager.instance = this;

        //Initialise the item database
        ItemDatabase.LoadDatabase();

        //Load the map prefab and initialize it
        var prefab = Resources.Load<GameObject>("Prefabs/TileMap");
        var instance = Instantiate(prefab);
        MapView = instance.GetComponent<ITileMapObject>();

        //create the map manager and pass it the map object
        MapManager = gameObject.AddComponent<TileMapManager>();
        MapManager.Initialize(MapView);

        selectionPrefab = Resources.Load<GameObject>("Prefabs/SelectedObject");
        //Initialize the starting state of the game
        StartGame();

    }


    void StartGame()
    {
        //create a list of maps, this is for testing purposes at the moment
        //MapHandler.NewMap(MapModel);
        List<ITileMapModel> models = new List<ITileMapModel>();
        models.Add(new TileMapTown(50, 50, models.Count));



        //give the map manager the list of maps we've made
        MapManager.SetUpTestWorld(models);

        characters = new List<Entity>();
        Entity characterTemp = null;
        //create the player
        characterTemp = new Entity(new PlayerInputComponent(), new PlayerMovementComponent());
        characterTemp.character = new PlayerCharacterComponent(characterTemp);
        characterTemp.Name = "Player " + 1;

        //print(UIManager.Instance);
        UIManager.Instance.SetCurrentPlayer(characterTemp);
        //create the enemies
        characters.Add(characterTemp);
        Cursor.instance.currentPlayer = characterTemp;


        currentCharacterIndex = 0;
        
    }

    private void Update()
    {
        foreach(Entity e in characters)
        {
            e.Update();
        }

        foreach (Entity c in MapManager.entities)
        {
            c.Update();
        }

    }

    public void ClearSelected()
    {

        if (selectedEntity != null)
        {
            Debug.Log("what");
            //selectedEntity.GetComponent<Selectable>().Select();

        }

    }
    
	
    public void CreateLibraries()
    {
        itemLibrary = new Dictionary<string, Item>();
        
        //itemLibrary.Add("Sword", )

    }

}
