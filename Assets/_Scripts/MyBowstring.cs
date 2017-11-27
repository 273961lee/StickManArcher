using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBowstring : MonoBehaviour {
    public Transform target;
    private LineRenderer self;
	// Use this for initialization
	void Start () {
        self = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        self.SetPosition(0,transform.position);
        self.SetPosition(1,target.position);
	}
}
