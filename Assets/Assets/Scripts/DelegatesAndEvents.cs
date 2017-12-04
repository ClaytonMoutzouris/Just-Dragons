using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DelegatesAndEvents : MonoBehaviour {

    public delegate void OnMapChanged(Exit e, ITileMapModel map);
    public static event OnMapChanged MapChanged;

    public static void TileMapChanged(Exit e, ITileMapModel map)
    {
        // does this delegate have at least one subscriber? 
        if (MapChanged != null)
        {
            MapChanged(e, map);
        }
    }

    public delegate void OnCharTileChanged(Tile t, Entity c);
    public static event OnCharTileChanged CharTileChanged;

    public static void CharacterTileChanged(Tile t, Entity c)
    {
        // does this delegate have at least one subscriber? 
        if (CharTileChanged != null)
        {
            CharTileChanged(t, c);
        }
    }

}
