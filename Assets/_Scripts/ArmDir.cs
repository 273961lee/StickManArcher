using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDir : MonoBehaviour {
    [SerializeField]
    private Vector3 touchPoint;
    [SerializeField]
    private Vector3 lookDir;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = new Vector3(temp.x, temp.y, 0);
            transform.LookAt(-dir);
        }
    }
}
