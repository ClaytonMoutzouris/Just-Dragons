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
    public Dictionary<int, Action> actionDatabase;
    List<CharacterData> charPrototypes;
    


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
        LoadCharData();
        StartGame();

    }

    void LoadCharData()
    {
        var loadData = Resources.LoadAll<CharacterData>("Character Data");
        charPrototypes = new List<CharacterData>();
        charPrototypes.AddRange(loadData);

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
        var prefab = Resources.Load<Entity>("Prefabs/Entity") as Entity;
        characterTemp = Instantiate(prefab);
        characterTemp.GetComponent<Entity>().Name = "Player " + 1;
        PlayerCharacter.CreateComponent(characterTemp.gameObject);



        //print(UIManager.Instance);
        UIManager.Instance.SetCurrentPlayer(characterTemp.GetComponent<PlayerCharacter>());
        //create the enemies
        characters.Add(characterTemp.GetComponent<Entity>());
        Cursor.instance.currentPlayer = characters[0].GetComponent<PlayerCharacter>();

        for (int i = 0; i < 10; i++)
        {

            characters.Add(CharacterGenerator.instance.CreateCharacter(charPrototypes[Random.Range(0, charPrototypes.Count)]));

        }
        
        currentCharacterIndex = 0;
        

        var structPrefab = Resources.Load<Entity>("Prefabs/Structure");
        Entity structureTemp = null;
        for (int i = 0; i < 2; i++)
        {
            structureTemp = Instantiate(structPrefab);
            structureTemp.GetComponent<Structure>().SetStructureParameters(Random.Range(1, 45), Random.Range(1, 45), 2, 2);
            structureTemp.GetComponent<Structure>().SetToTiles();
            structureTemp.GetComponent<Entity>().Name = "Struct " + i;

            //structureTemp.Add
        }

        var objPrefab = Resources.Load<Entity>("Prefabs/Tree");
        Entity objTemp = null;
        for (int i = 0; i < 5; i++)
        {
            objTemp = Instantiate(objPrefab);
            objTemp.GetComponent<Entity>().SetToTile(MapManager.GetTile(Random.Range(1,48), Random.Range(1,48)));
            objTemp.GetComponent<Entity>().Name = "Tree " + i;

        }


        //UIManager.Instance.SetCurrentPlayer(characters[0].GetComponent<Player>());
        //UIManager.Instance.
        //Camera.main.GetComponent<CameraController>().target = characters[0].transform;
    }

    private void Update()
    {

        

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
