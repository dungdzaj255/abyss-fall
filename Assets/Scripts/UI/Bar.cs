using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {
    [SerializeField] private Slider slider;
    [SerializeField] private Text textDisplay;
    public void InitBar(int initValue) {
        slider.value = initValue;
    }
    public void SetMax(int maxValue) {
        slider.maxValue = maxValue;
    }
    public void SetCurrent(int value) {
        slider.value = value;
    }

    public void SetText(string text) {
        textDisplay.text = text;
    }
}
