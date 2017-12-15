using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelectionIndicator : MonoBehaviour {
    public static TileSelectionIndicator Instance;
	// Use this for initialization
	void Start () {

        if (Instance != null)
            Destroy(this);

        Instance = this;
	}
	

}
