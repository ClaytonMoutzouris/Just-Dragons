using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Activation { Button, Action, Automatic };
public enum Targeting { Self, CurrentTarget, AutoSelect }; // et cetera for other settings.


//=== Individual Skills: =============================

[System.Serializable]
public class Action : ScriptableObject
{
    [SerializeField]
    public Sprite image;
    [SerializeField]
    public string skillName = string.Empty; // I always initialize variables.
    [SerializeField]
    public float mpCost = 0;
    [SerializeField]
    public Activation activation = Activation.Button;
    [SerializeField]
    public string activationButton = "Fire1";
    [SerializeField]
    public Targeting targeting = Targeting.CurrentTarget;
    // etc.
    [SerializeField]
    public bool isActive = false;

    /*
    public void Update(MonoBehaviour parentMonoBehaviour)
    {
        if (!isActive && CanActivate())
        {
            //parentMonoBehaviour.StartCoroutine(Use());
        }
    }
    */
    
    public bool CanActivate()
    {
        switch (activation)
        {
            case Activation.Button: return Input.GetButtonDown(activationButton);
                // etc.
        }

        return true;
    }

    
    public void Use()
    { // Coroutine so it can do stuff over time.
        //isActive = true;
        // Do stuff based on the settings above.
        //isActive = false;
        
    }
    
}
