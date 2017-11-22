using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraetEnemy : MonoBehaviour {
    public static CraetEnemy instance;
    public GameObject enemy;
    public Transform[] points;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CreatNext() {
        Instantiate(enemy,Vector3.zero,Quaternion.identity);
    }
}
