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
    public float zoom = 10;

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
        transform.position = new Vector3(x, y, 0) + offset;
    }

    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, y, 0) + offset;
    }

    void LateUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            offset.z += Input.GetAxis("Mouse ScrollWheel") * 10;
            offset.z = Mathf.Clamp(offset.z, -12.5f, -5.0f);
            Debug.Log(offset.z);
        }

        desiredPosition = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }

    void updateZoom()
    {

    }
}