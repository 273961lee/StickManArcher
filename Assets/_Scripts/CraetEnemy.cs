using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraetEnemy : MonoBehaviour {
    public static CraetEnemy instance;
    public bool isOn=true;
    public GameObject enemy;
    public float during;
    public GameObject creater;
    public List<Vector3> points=new List<Vector3>();
    public int sign = 0;
    public int enemyNums = 1;
    public GameObject lifeItem;
    private Vector3 rect;
    // Use this for initialization
    private void OnLevelWasLoaded(int level)
    {
        if (level!=-1)
        {
            isOn = true;
        }
    }

    //IEnumerator ItemCreat() {
    //    yield return new WaitForSeconds(10);
    //    Instantiate(lifeItem,new Vector3(Random.Range(0,rect.x),Random.Range(rect.y,)),Quaternion.identity);
    //    StartCoroutine(ItemCreat());
    //}
    void Start () {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < creater.transform.childCount; i++)
        {
            points.Add(creater.transform.GetChild(i).position);
        }
        rect = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,-Camera.main.transform.position.z));
        StartCoroutine(CreatTimer());
        //StartCoroutine(ItemCreat());
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyNums>=5)
        {
            isOn = false;
        }
        else
        {
            isOn = true;
        }
	}
    public void UpdateNums() {
        enemyNums -= 1;
    }

    public void UpdatePoints(Vector3 pos) {
        points.Add(pos);
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
        if (sign<points.Count-1)
        {
            sign++;
        }
        else
        {
            sign = 0;
        }
        if (isOn)
        {
            GameObject temp= Instantiate(enemy, points[sign], Quaternion.identity);
            temp.GetComponent<EnemyNew>().sign = sign;
            enemyNums += 1;
            points.RemoveAt(sign);
        }
    }
}
