using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake(){
        Instance = this;
    }

    public GameObject pfItemWorld;

    public Sprite swordSprite;
    public Sprite coinSprite;
    public Sprite potionSprite;
}