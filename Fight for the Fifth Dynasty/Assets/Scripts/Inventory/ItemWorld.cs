using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item) {
        GameObject transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetItem(Item item){
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        
        if (item.GetAnimation() != null){
            animator = GetComponent<Animator>();
        }

        if (item.GetAnimation() != null){
            animator.runtimeAnimatorController = item.GetAnimation();
        }
    }

    public Item GetItem(){
        return item;
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }
}
