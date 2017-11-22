using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorSetter : MonoBehaviour {
    private SpriteRenderer rendColor;
    private Color32 color;
    private float num=0;

    // Use this for initialization
    void Start () {
        rendColor = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeColor() {
        if (num>=1)
        {
            num = 0;
        }
        color = Color.HSVToRGB(num+=0.1f,0.5f,1f);
        rendColor.color = color;
    }
}
