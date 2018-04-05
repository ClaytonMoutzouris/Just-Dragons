using UnityEditor;
using UnityEngine;

public class SkillMenu {

    [MenuItem("Assets/Create/Spells/Damage Spells")]
    public static void CreateDamageSpellAsset()
    {
        ScriptableObjectUtility.CreateAsset<DamageSpell>();
    }

}
