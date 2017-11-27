using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour {

    private LineRenderer line;
    public SpriteRenderer touchIcon;
    private Vector3 startPoint;
    private Vector3 endPoint;
	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tempPos = new Vector3(tempPos.x,tempPos.y,0);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startPoint = tempPos;
            line.SetPosition(0,startPoint);
            touchIcon.enabled = true;
            touchIcon.gameObject.transform.position = startPoint;

        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            endPoint = tempPos;
            line.SetPosition(1,endPoint);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            touchIcon.enabled = false;
            startPoint = new Vector3(0,0,0);
            endPoint = new Vector3(0,0,0);
            line.SetPosition(0, startPoint);
            line.SetPosition(1, endPoint);
        }
    }
}
