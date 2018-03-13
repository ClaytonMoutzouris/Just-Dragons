using UnityEditor;
using UnityEngine;

public class ItemEditor
{

    [MenuItem("Assets/Create/Item")]
    public static void CreateSkillAsset()
    {
        ScriptableObjectUtility.CreateAsset<Item>();
    }


}
