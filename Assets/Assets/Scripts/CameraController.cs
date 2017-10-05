using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public static CameraController instance;

    private Vector3 offset;
    int speed;

    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
        }

        instance = this;

        speed = 25;
    }

    public void SetToTile(int x, int y)
    {
        transform.position = new Vector3(Mathf.CeilToInt(x * 1), Mathf.CeilToInt(y * 1), -10);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }
}