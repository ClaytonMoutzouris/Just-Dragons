using UnityEditor;
using UnityEngine;

public class ItemEditor
{

    [MenuItem("Assets/Create/Item")]
    public static void CreateBaseItemAsset()
    {
        ScriptableObjectUtility.CreateAsset<Item>();
    }

    [MenuItem("Assets/Create/Item/Consumable Item/HealthPotion")]
    public static void CreateHealthPotionAsset()
    {
        ScriptableObjectUtility.CreateAsset<HealthPotion>();
    }

    [MenuItem("Assets/Create/Item/Consumable Item/TransformPotion")]
    public static void CreateTransformnPotionAsset()
    {
        ScriptableObjectUtility.CreateAsset<TransformPotion>();
    }


}
