using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEvent : MonoBehaviour {
    private GameObject player;
    private void OnLevelWasLoaded(int level)
    {
        if (level!=-1)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    private void OnEnable()
    {
        print("enable here");
        if (player!=null)
        {
            player.GetComponent<Shoter>().isGamePlaying = false;

        }
    }
    private void OnDisable()
    {
        if (player!=null)
        {
            player.GetComponent<Shoter>().isGamePlaying = true;

        }
        GameMenu.instance.ResetPlayerAche();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
