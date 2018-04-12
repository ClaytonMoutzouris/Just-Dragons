using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawComponent {

    public GameObject entity;
    public SpriteRenderer sRenderer;
    public GameObject selected;

    public DrawComponent(Entity entity)
    {
        this.entity = new GameObject();
        sRenderer = this.entity.AddComponent<SpriteRenderer>();
        sRenderer.sprite = Resources.Load<Sprite>("Textures and Sprites/CharacterSprite_1");
        this.entity.layer = LayerMask.NameToLayer("Characters");

        selected = Object.Instantiate(GameManager.instance.selectionPrefab, this.entity.transform.position, Quaternion.identity, this.entity.transform);
        selected.SetActive(false);

    }


    public virtual void Update(Entity entity)
    {

        this.entity.transform.position = entity.worldPosition;
        selected.SetActive(entity.isSelected);
    }

}
