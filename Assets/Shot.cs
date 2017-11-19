using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using DG.Tweening;

public class Shot : MonoBehaviour {
    public GameObject pool;
    public GameObject arrowNext;
    public float speed;
    public Vector3 dir;
    private Vector3 playerPos;
    private Vector3 randomOffset;
	// Use this for initialization
	void Start () {
        if (gameObject.CompareTag("Enemy"))
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            StartCoroutine(ShotTimer());
        }
	}

    IEnumerator ShotTimer() {
        yield return new WaitForSeconds(2);
        randomOffset = new Vector3(Random.Range(0,10), Random.Range(0, 10), 0);
        GameObject tempArrow = Instantiate(arrowNext,transform.position,Quaternion.identity);
        Vector3 tempPos = playerPos.normalized;
        tempArrow.GetComponent<Rigidbody2D>().simulated = true;
        tempArrow.GetComponent<Rigidbody2D>().velocity = speed * tempPos+randomOffset;
        StartCoroutine(ShotTimer());
    }
	
	// Update is called once per frame
	void Update () {
        dir = Input.mousePosition;
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&gameObject.CompareTag("Player"))
        {
            GameObject tempArrow= Instantiate(arrowNext,transform.position,Quaternion.identity);
            Vector3 tempPos = Camera.main.ScreenToWorldPoint(dir).normalized;
            //tempArrow.transform.SetParent(pool.transform);
            tempArrow.GetComponent<Rigidbody2D>().simulated = true;
            tempArrow.GetComponent<Rigidbody2D>().velocity = speed * tempPos;
        }
    }
}
