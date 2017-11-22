using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraetEnemy : MonoBehaviour {
    public static CraetEnemy instance;
    public GameObject enemy;
    public GameObject[] points=new GameObject[5];
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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CreatNext() {
        Instantiate(enemy,points[Random.Range(0,4)].transform.position,Quaternion.identity);
    }
}
