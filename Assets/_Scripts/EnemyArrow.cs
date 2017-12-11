using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour {

    private Rigidbody rig;
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        rig.transform.localRotation = Quaternion.LookRotation(rig.velocity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag!="Enemy"&&other.tag!="EnemyArrow")
        {
            rig.isKinematic = true;
            Destroy(gameObject,3.0f);
        }
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Shoter>().ChangeLife(-2);
        }
    }
}
