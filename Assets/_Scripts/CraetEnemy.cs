using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraetEnemy : MonoBehaviour {
    public static CraetEnemy instance;
    public bool isOn=true;
    public GameObject enemy;
    public float during;
    public GameObject[] points=new GameObject[5];
    public int sign = 0;
    // Use this for initialization
    private void OnLevelWasLoaded(int level)
    {
        if (level!=-1)
        {
            isOn = true;
        }
    }
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
        if (sign<points.Length-1)
        {
            sign++;
        }
        else
        {
            sign = 0;
        }
        if (isOn)
        {
            Instantiate(enemy, points[sign].transform.position, Quaternion.identity);
        }
    }
}
