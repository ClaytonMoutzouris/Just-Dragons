using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//=== Individual Skills: =============================

public enum CharacterRace { Human, Goblin, Elf, Dwarf };

[System.Serializable]
public class NPCPrototype : ScriptableObject
{
    [SerializeField]
    public int charID;
    [SerializeField]
    public Sprite image;
    [SerializeField]
    public string characterName = string.Empty; // I always initialize variables.
    [SerializeField]
    public Hostility hostility;
    [SerializeField]
    CharacterRace race;

}
