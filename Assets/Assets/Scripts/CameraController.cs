using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public GameObject player;
    public static CameraController instance;
    public float smoothSpeed = 0.125f;
    private Vector3 offset = new Vector3(0,0,-10);
    public Vector3 desiredPosition;
    public Vector3 smoothedPosition;

    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
        }

        instance = this;


    }

    public void SetToTile(int x, int y)
    {
        transform.position = new Vector3(x, y, -10);
    }

    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, y, -10);
    }

    void LateUpdate()
    {

        desiredPosition = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
}