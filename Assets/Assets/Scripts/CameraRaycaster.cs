using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Ground,
        Layer.Objects,
        Layer.Characters

    };

    float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit2D m_hit2D;
    public RaycastHit2D hit2D
    {
        get { return m_hit2D; }
    }

    RaycastHit m_hit;
    public RaycastHit hit
    {
        get { return m_hit; }
    }
    Layer m_layerHit;
    public Layer layerHit
    {
        get { return m_layerHit; }
    }

    void Start() // TODO Awake?
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            m_hit.distance = distanceToBackground;
            m_layerHit = Layer.RaycastEndStop;
            return;
        }
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {

                
                var hit2D = RaycastForLayer2D(layer);
                if (hit2D.HasValue)
                {
                    m_hit2D = hit2D.Value;
                    m_layerHit = layer;
                print(layer);
                    return;
                }          
            

        }

        // Otherwise return background hit
        m_hit.distance = distanceToBackground;
        m_layerHit = Layer.RaycastEndStop;
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {

        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {

            return hit;
        }
        return null;
    }

    RaycastHit2D? RaycastForLayer2D(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        //Converting Mouse Pos to 2D (vector2) World Pos
        //Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 100f, layerMask);
        
        if (hit)
        {
           

            return hit;
        }
        else return null;
    }
}
