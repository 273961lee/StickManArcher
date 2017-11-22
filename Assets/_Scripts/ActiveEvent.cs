using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEvent : MonoBehaviour {

    private void OnEnable()
    {
        GameMenu.instance.Press(0.0001f);
    }
    private void OnDisable()
    {
        GameMenu.instance.Press(1.0f);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
