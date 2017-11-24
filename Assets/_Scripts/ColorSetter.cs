using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorSetter : MonoBehaviour {
    private SpriteRenderer rendColor;
    private Color32 color;
    // Use this for initialization
    void Start () {
        rendColor = GetComponent<SpriteRenderer>();
        ChangeColor();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeColor() {
        float value = Random.Range(0f, 1f);
        print(value);
        color = Color.HSVToRGB(value, 0.5f, 1f);
        print("color set");
        rendColor.color = color;
        print("color set scussful");
    }
}
