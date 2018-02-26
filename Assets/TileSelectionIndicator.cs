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

    public void SetPosition(Vector2 pos)
    {
        transform.SetPositionAndRotation(pos, Quaternion.identity);
    }
	

}
