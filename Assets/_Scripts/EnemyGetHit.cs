using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour {
    private Animator getHit;
	// Use this for initialization
	void Start () {
        getHit = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseEnemyClip() {
        getHit.SetBool("isHit",false);
    }
}
