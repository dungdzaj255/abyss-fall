using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour {
    public static PointSystem instance;

    [SerializeField] private Text text;
    public int currentPoint = 0;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        text.text = currentPoint.ToString();
    }

    public void AddPoint(int point) {
        currentPoint += point;
        text.text = currentPoint.ToString();
    }
}
