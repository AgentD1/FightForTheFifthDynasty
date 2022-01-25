using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour {
	private Slider slider;

	// Start is called before the first frame update
	void Start() {
		slider = GetComponent<Slider>();
	}

	// Update is called once per frame
	void Update() {
		float fillValue = Player.instance.health;
		slider.value = fillValue;

		if (fillValue < 0) {
			fillValue = 0;
		}
	}
}
