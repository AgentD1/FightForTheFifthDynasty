using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType {
        Potion,
        Sword,
        Coin,
    }

    public ItemType itemType;
    public int amount;
}
