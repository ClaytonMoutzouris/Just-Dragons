using UnityEditor;
using UnityEngine;

public class SkillMenu {


    [MenuItem("Assets/Create/Actions/Attack Action")]
    public static void CreateSkillAsset()
    {
        ScriptableObjectUtility.CreateAsset<AttackAction>();
    }

}
