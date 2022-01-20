using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
  
    public List<Image> HeartList = new List<Image>();

    public Image heart;
    public Image blank;

    public void Start(){

        HeartList.Add(Heart1);
    }

    public void Update()  {

        for (int i = 0; i < 5; i++) {
            if (Player.health >= i){
                HeartList[i] = heart;

            }
            else {
                HeartList[i] = blank;

            }
        }

    }   
}