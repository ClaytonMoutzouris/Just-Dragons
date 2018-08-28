using UnityEditor;
using UnityEngine;

public class ItemEditor
{

    [MenuItem("Assets/Create/Item")]
    public static void CreateBaseItemAsset()
    {
        ScriptableObjectUtility.CreateAsset<Item>();
    }

    [MenuItem("Assets/Create/Item/Consumable Item/Health Potion")]
    public static void CreateHealthPotionAsset()
    {
        ScriptableObjectUtility.CreateAsset<HealthPotion>();
    }

    [MenuItem("Assets/Create/Item/Consumable Item/Transform Potion")]
    public static void CreateTransformnPotionAsset()
    {
        ScriptableObjectUtility.CreateAsset<TransformPotion>();
    }


}
