using UnityEditor;
using UnityEngine;

public class NPCMenu
{

    [MenuItem("Assets/Create/Character Prototype")]
    public static void CreateSkillAsset()
    {
        ScriptableObjectUtility.CreateAsset<NPCPrototype>();
    }


}
