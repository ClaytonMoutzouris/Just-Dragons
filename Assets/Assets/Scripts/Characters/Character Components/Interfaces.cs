using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILootable
{
    List<Item> GetLoot();
}

public interface ICombatant
{

}

public interface IEntity
{

    string EntityName { get; set; }
    Tile CurrentTile { get; set; }
    void SetToTile(Tile t);
    DrawComponent Graphics { get; set; }
    MovementComponent Movement { get; set; }
    void Select();
    Stats Stats { get; set; }
    string GetTooltip();
    void Update();

}
