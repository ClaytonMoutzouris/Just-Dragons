using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawComponent {

    public GameObject entity;
    public SpriteRenderer sRenderer;
    public GameObject selected;

    public DrawComponent()
    {
        entity = new GameObject();
        sRenderer = entity.AddComponent<SpriteRenderer>();
        sRenderer.sprite = Resources.Load<Sprite>("Textures and Sprites/CharacterSprite_1");
        entity.layer = LayerMask.NameToLayer("Characters");

        selected = Object.Instantiate(GameManager.instance.selectionPrefab, entity.transform.position, Quaternion.identity, entity.transform);
        selected.SetActive(false);

    }


    public virtual void Update()
    {
       //this.IEntity.transform.position = IEntity.worldPosition;
        //selected.SetActive(IEntity.isSelected);
    }

    public virtual Vector3 GetWorldPosition()
    {
        return entity.transform.position;
    }

    public virtual void SetWorldPosition(Vector3 pos)
    {
        entity.transform.position = pos;
    }

    public virtual void Destroy()
    {
        Object.Destroy(entity);
    }
}
