using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraetEnemy : MonoBehaviour {
    public static CraetEnemy instance;
    public GameObject enemy;
    public float during;
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
        StartCoroutine(CreatTimer());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator CreatTimer() {
        during -= GameMenu.instance.score * 0.01f;
        if (during<=1)
        {
            during = 1;
        }
        yield return new WaitForSeconds(during);
        CreatNext();
        StartCoroutine(CreatTimer());
    }
    public void CreatNext() {
        Instantiate(enemy,points[Random.Range(0,4)].transform.position,Quaternion.identity);
    }
}
