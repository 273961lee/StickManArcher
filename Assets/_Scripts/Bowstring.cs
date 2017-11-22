using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowstring : MonoBehaviour {
    public GameObject hand;
    public Transform endPoint;
    public Vector3 tempPos;
    private LineRenderer selfLine;
	// Use this for initialization
	void Start () {
        selfLine = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        selfLine.SetPosition(1,hand.transform.position);
        selfLine.SetPosition(0,endPoint.position);
	}
}
