using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TempPlayer : MonoBehaviour {
    public Vector3 touchPoint;
    public GameObject player;
    public GameObject bullet;
    public Rigidbody tempRig;
    public GameObject point2;
    public GameObject father;
    public GameObject cube3;
    public bool isLook;
	// Use this for initialization
	void Start () {
        tempRig = bullet.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        touchPoint = Input.mousePosition;
        touchPoint = Camera.main.ScreenToWorldPoint(touchPoint);
        touchPoint = new Vector3(0,touchPoint.y,touchPoint.z);
        player.transform.DOLookAt(touchPoint,0.2f);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            bullet.transform.SetParent(father.transform);
            cube3.transform.SetParent(father.transform);
            tempRig.centerOfMass = point2.transform.position;
            tempRig.useGravity = true;
            tempRig.velocity = bullet.transform.forward * 20;
            isLook = true;
        }
        if (isLook)
        {
            
        }
    }
}
