using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyNew : MonoBehaviour {
    public GameObject headIcon;
    public Rigidbody2D[] rigs;//身体刚体
    public HingeJoint2D[] joints;//身体骨骼链
    public PolygonCollider2D[] collisions;//身体碰撞器
    public int life;//生命值
    public SpriteRenderer lifeBar;//生命条
    public GameObject arrow;//预制体
    public GameObject tempArrow;//预制体克隆
    public GameObject arrowStartPos;
    public Vector3 shotDir;
    public bool isDead;
    public bool isIce;
    public GameObject arm;
    public GameObject pool;
    public Animator clip;
    public int sign;
    public GameObject ice;
    public ParticleSystem fire;
    public Shoter player;

    // Use this for initialization
    void Start () {
        SwichPhyics(true);
        pool = GameObject.FindGameObjectWithTag("Pool");
        //Debug.LogError("我的编号是"+sign);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoter>();
        StartCoroutine(LoopShot());
        
    }

    IEnumerator LoopShot() {
        yield return new WaitForSeconds(2);
        if (!isIce)
        Aim();
        yield return new WaitForSeconds(1.5f);
        if (!isIce)
        Shot();
        if (!isDead&&!isIce)
        {
            StartCoroutine(LoopShot());
        }
    }

    public void Aim() {
        tempArrow=Instantiate(arrow,arm.transform.position,arm.transform.rotation);
        tempArrow.transform.SetParent(arm.transform);
        arm.transform.DORotate(new Vector3(Random.Range(-40,18),-90,0),1.0f);
    }
    public void Shot() {
        if (!isDead)
        {
            tempArrow.transform.SetParent(pool.transform);
            tempArrow.GetComponent<Rigidbody>().isKinematic = false;
            tempArrow.GetComponent<Rigidbody>().velocity = tempArrow.transform.forward * Random.Range(9,18);
        }
    }
    public void SubLife(int num) {
        if (life>0)
        {
            life -= num;
            clip.SetBool("isHit",true);
            lifeBar.size = new Vector2(life*0.1f,0.2f);
        }
        else
        {
            Dead();
        }
        
    }
    public void SwichPhyics(bool args)
    {
        foreach (HingeJoint2D item in joints)
        {
            item.enabled = !args;
        }
        foreach (Rigidbody2D item in rigs)
        {
            if (args == true)
            {
                item.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                item.bodyType = RigidbodyType2D.Dynamic;
            }

        }
        foreach (PolygonCollider2D item in collisions)
        {
            item.isTrigger = args;
        }
    }
    public void Dead()
    {
        StopCoroutine(LoopShot());
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        headIcon.transform.DOMoveY(transform.position.y + 3, 1.0f).OnComplete(() => headIcon.GetComponent<SpriteRenderer>().DOFade(0, 0.2f));
        GameMenu.instance.AddScore(10);
        SwichPhyics(false);
        Destroy(gameObject,1.5f);
    }
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        CraetEnemy.instance.UpdateNums();
        CraetEnemy.instance.UpdatePoints(transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.GetComponent<IceArrow>()!=null)
        {
            isIce = true;
            ice.transform.DOLocalMoveZ(-1f,0.5f);
        }
        if (other.transform.parent.GetComponent<FireArrow>()!=null)
        {
            fire.Play();
            StartCoroutine(FireStart());
        }
    }
    IEnumerator FireStart() {
        yield return new WaitForSeconds(1);
        GetComponent<Shoter>().ChangeLife(-1);
        StartCoroutine(FireStart());
    }
}
