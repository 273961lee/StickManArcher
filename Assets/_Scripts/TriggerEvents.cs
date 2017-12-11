using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvents : MonoBehaviour {

    public GameObject headShot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerArrow"))
        {
            GameObject temp= Instantiate(headShot,transform.position+new Vector3(0,0.2f,0),Quaternion.identity);
            Destroy(temp,1.0f);
            transform.parent.GetComponent<Shoter>().ChangeLife(-10);
        }
    }
}
