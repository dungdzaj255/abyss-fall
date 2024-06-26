using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {
    [SerializeField] private Slider slider;

    public void SetMax(int maxValue) {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }
    public void SetCurrent(int value) {
        slider.value = value;
    }
}
