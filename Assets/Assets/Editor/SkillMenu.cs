using UnityEditor;
using UnityEngine;

public class SkillMenu {

    [MenuItem("Assets/Create/Skill")]
    public static void CreateSkillAsset()
    {
        ScriptableObjectUtility.CreateAsset<Action>();
    }

    [MenuItem("Assets/Create/Skill Database")]
    public static void CreateSkillDatabaseAsset()
    {
        ScriptableObjectUtility.CreateAsset<ActionDatabase>();
    }
}
