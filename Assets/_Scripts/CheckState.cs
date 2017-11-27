using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckState : MonoBehaviour {
    private Rigidbody2D rig;
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.name);
    }
}
