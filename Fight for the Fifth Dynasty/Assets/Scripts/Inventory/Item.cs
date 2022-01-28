using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        Potion,
        Sword,
        Coin,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite(){
        switch (itemType){
            case ItemType.Sword:    return ItemAssets.Instance.swordSprite;
            case ItemType.Coin:     return ItemAssets.Instance.coinSprite;
            case ItemType.Potion:   return ItemAssets.Instance.potionSprite;
        }
        return null;
    }
}
