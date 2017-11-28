using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEvent : MonoBehaviour {

    private void OnEnable()
    {
        print("enable here");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Shoter>().isGamePlaying=false;
    }
    private void OnDisable()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Shoter>().isGamePlaying = true;
        GameMenu.instance.ResetPlayerAche();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
