using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour {
    public static DrawLines instance;
    private LineRenderer line;
    public SpriteRenderer touchIcon;
    private Vector3 startPoint;
    private Vector3 endPoint;
    public Transform archPos;
    public bool isDefaultMod=false;
	// Use this for initialization
	void Start () {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        line = GetComponent<LineRenderer>();
	}

    private void DrawGuideLine(bool type) {
        Vector3 tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tempPos = new Vector3(tempPos.x, tempPos.y, 0);
        if (type)
        {
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
                line.SetPosition(1, endPoint);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                touchIcon.enabled = false;
                startPoint = new Vector3(0, 0, 0);
                endPoint = new Vector3(0, 0, 0);
                line.SetPosition(0, startPoint);
                line.SetPosition(1, endPoint);
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //startPoint = archPos.position;
                //line.SetPosition(0,startPoint);
                touchIcon.enabled = true;
                //touchIcon.gameObject.transform.position = startPoint;

            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                startPoint = archPos.position;
                line.SetPosition(0, startPoint);
                touchIcon.gameObject.transform.position = startPoint;

                endPoint = tempPos;
                line.SetPosition(1, endPoint);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                touchIcon.enabled = false;
                startPoint = new Vector3(0, 0, 0);
                endPoint = new Vector3(0, 0, 0);
                line.SetPosition(0, startPoint);
                line.SetPosition(1, endPoint);
            }

        }

    }

    // Update is called once per frame
    void Update () {
        DrawGuideLine(isDefaultMod);
    }
}
