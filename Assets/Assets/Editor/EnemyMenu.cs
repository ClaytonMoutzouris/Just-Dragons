using UnityEditor;
using UnityEngine;

public class EnemyMenu {

    [MenuItem("Assets/Create/CharacterData")]
    public static void CreateSkillAsset()
    {
        ScriptableObjectUtility.CreateAsset<CharacterData>();
    }


}
