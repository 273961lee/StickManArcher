using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShotAnimation : MonoBehaviour {
    private Vector3 startPos;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayShotAnimation(float offset) {
        transform.DOMoveX(transform.position.x-offset,0.3f).OnComplete(()=>transform.DOMoveX(transform.position.x+offset,0.2f));
        //print("animtion is played.....");
    }
}
