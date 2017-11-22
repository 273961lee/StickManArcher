using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
    public Shoter self;
    public int damageValue;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(gameObject.tag))
        {
            collision.GetComponent<Rigidbody2D>().simulated = false;
            self.ChangeLife(-damageValue);
        }
    }
}
