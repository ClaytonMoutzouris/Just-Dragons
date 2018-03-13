using UnityEditor;
using UnityEngine;

public class SkillMenu {

    [MenuItem("Assets/Create/Action")]
    public static void CreateSkillAsset()
    {
        ScriptableObjectUtility.CreateAsset<Action>();
    }

    [MenuItem("Assets/Create/Action Database")]
    public static void CreateSkillDatabaseAsset()
    {
        ScriptableObjectUtility.CreateAsset<ActionDatabase>();
    }
}
