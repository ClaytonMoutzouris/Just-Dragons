using UnityEditor;
using UnityEngine;

public class SkillMenu {


    [MenuItem("Assets/Create/Actions/Attack Action")]
    public static void CreateAttackActionAsset()
    {
        ScriptableObjectUtility.CreateAsset<AttackAction>();
    }

    [MenuItem("Assets/Create/Actions/Guard Action")]
    public static void CreateGuardActionAsset()
    {
        ScriptableObjectUtility.CreateAsset<GuardAction>();
    }

}
