using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qarth;
using DG.Tweening;

public class Shot : MonoBehaviour {
    public GameObject pool;//对象池 todo
    public GameObject arm;//手臂
    public GameObject arrowNext;//箭的预制体
    public float speed;//射速
    public Vector3 dir;//方向
    private Vector3 playerPos;//玩家位置
    private Vector3 randomOffset;//bot射击偏移值
    private float offset_X=8;//偏移分量
    private float offset_Y=8;//偏移分量
    private bool isTouch;//是否开始触摸
	// Use this for initialization
	void Start () {
        if (gameObject.CompareTag("Enemy"))
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            arm = transform.root.Find("sitckman_0006_Arm_out").gameObject;
            StartCoroutine(ShotTimer());
        }
	}
    //bot循环射击
    IEnumerator ShotTimer() {
        yield return new WaitForSeconds(2);
        randomOffset = new Vector3(Random.Range(0,offset_X), Random.Range(0, offset_Y), 0);
        GameObject tempArrow = Instantiate(arrowNext,transform.position,Quaternion.identity);
        tempArrow.GetComponent<Rigidbody2D>().simulated = true;
        tempArrow.GetComponent<Rigidbody2D>().velocity = speed * (playerPos-transform.position).normalized;
        arm.GetComponent<ShotAnimation>().PlayShotAnimation(-0.5f);
        StartCoroutine(ShotTimer());
    }
	
	// Update is called once per frame
	void Update () {
        dir = Input.mousePosition;
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0)&&gameObject.CompareTag("Player"))
        {
            isTouch = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)&&isTouch)
        {
            arm.GetComponent<ShotAnimation>().PlayShotAnimation(0.5f);
            PlayShot();
            isTouch = false;
        }
    }
    //射击
    private void PlayShot() {
        GameObject tempArrow = Instantiate(arrowNext, transform.position, transform.rotation);
        Vector3 tempPos = Camera.main.ScreenToWorldPoint(dir).normalized;
        tempArrow.GetComponent<Rigidbody2D>().simulated = true;
        tempArrow.GetComponent<Rigidbody2D>().velocity = speed * -tempPos;
    }
}
