using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Player player;
    public Image fillImage;
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {   
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float fillValue = Player.health / Player.maxHealth;
        slider.value = fillValue;

        if (fillValue < 0 ){
            fillValue = 0;
        }
    }
}
