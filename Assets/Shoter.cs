using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shoter : MonoBehaviour {
    public GameObject headIcon;
    public Rigidbody2D[] rigs;
    public HingeJoint2D[] joints;
    public PolygonCollider2D[] collisions;
    public int life;//生命值
    public SpriteRenderer lifeBar;
    //改变生命值
    public int ChangeLife(int value) {
        SoundBase.instance.PlayHitAU();
        life += value;
        if (life<0)
        {
            life = 0;
            Dead();
        }
        lifeBar.size = new Vector2(life*0.1f,0.2f);
        return life;
    }
    //是否死亡
    public bool isDead;
    public bool Dead() {
        SoundBase.instance.PlayDeadAU();
        SwithPhyics(false);
        if (isPlayer)
        {
            GameMenu.instance.GameOver();
        }
        if (!isPlayer)
        {
            headIcon.transform.DOMoveY(transform.position.y+3,1.0f).OnComplete(()=>headIcon.GetComponent<SpriteRenderer>().DOFade(0,0.2f));
            GameMenu.instance.AddScore(10);
            Destroy(gameObject, 2.0f);
        }
        return isDead = true;
    }

    public float shotSpeed;
    public List<Vector3> tempPoint = new List<Vector3>();
    public void ChangShotSpeed() {
        shotSpeed = 0;
        shotSpeed = Vector3.Distance(tempPoint[0],tempPoint[1])*0.1f;
    }

    public Vector3 shotDir;

    public bool isTouch=false;
    public Vector3 touchPoint;

    public GameObject arm;
    public Vector3 aimDir;
    public Transform aimPoint;
    public Transform arch;
    public Animator armMotion;

    public GameObject pool;

    public GameObject player;
    public GameObject powerArrow;
    public void Shot() {
        Rigidbody2D tempRig = arrowTemp.GetComponent<Rigidbody2D>();
        print("get rig");
        shotDir = (Camera.main.ScreenToWorldPoint(touchPoint)+new Vector3(0,0,10)/*摄像机差值*/ - aimPoint.position).normalized;
        tempRig.simulated = true;
        tempRig.velocity = shotSpeed*shotDir;
        tempRig.transform.SetParent(pool.transform);
        if (GameMenu.instance.isPowerFul)
        {
            arrowTemp = Instantiate(powerArrow, arrowTexture.transform.position, arrowTexture.transform.rotation);
            arrowTemp.transform.SetParent(arch);
        }
        else
        {
            arrowTemp = Instantiate(arrowPrefab, arrowTexture.transform.position, arrowTexture.transform.rotation);
            arrowTemp.transform.SetParent(arch);
        }
        print("shot");
    }
    public void Shot(bool defaultModle) {
        Rigidbody2D tempRig = arrowTemp.GetComponent<Rigidbody2D>();
    }
    IEnumerator LoopShot() {
        /*float timer = 40 / GameMenu.instance.score;
        if (timer<1)
        {
            timer = 1;
        }*/
        arm.transform.DOLookAt(new Vector3(player.transform.position.x,player.transform.position.y+2,0),1.0f);
        yield return new WaitForSeconds(2);
        Shot(1);
        StartCoroutine(LoopShot());
    }
    public void Shot(int type) {
        if (type==1)
        {
            Rigidbody2D tempRig = arrowTemp.GetComponent<Rigidbody2D>();
            shotDir = (player.transform.position - transform.position).normalized;
            tempRig.simulated = true;
            tempRig.velocity = Random.Range(10,25) * shotDir+(Vector3)Random.insideUnitCircle;
            tempRig.transform.SetParent(pool.transform);
            arrowTemp = Instantiate(arrowPrefab, arrowTexture.transform.position, arrowTexture.transform.rotation);
            arrowTemp.GetComponent<SpriteRenderer>().flipX = true;
            arrowTemp.transform.localScale = new Vector3(arrowTemp.transform.localScale.x, arrowTemp.transform.localScale.y, -arrowTemp.transform.localScale.z);
            arrowTemp.transform.SetParent(arch);
        }
    }

    public bool isPlayer;

    public GameObject arrowPrefab;
    public GameObject arrowTemp;

    public GameObject arrowTexture;
	// Use this for initialization
	void Start () {
        isPlayer = gameObject.CompareTag("Player");
        player = GameObject.FindGameObjectWithTag("Player");
        pool = GameObject.FindGameObjectWithTag("BackGround");
        SwithPhyics(true);
        if (!isPlayer)
        {
            StartCoroutine(LoopShot());
        }
        print(isPlayer);
	}

    public void SwithPhyics(bool args) {
        foreach (HingeJoint2D item in joints)
        {
            item.enabled = !args;
        }
        foreach (Rigidbody2D item in rigs)
        {
            if (args==true)
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
    // Update is called once per frame
    void Update () {
        if (isPlayer&&!isDead)
        {
            //test function
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Dead();
            }
            touchPoint = Input.mousePosition;
            aimDir = Camera.main.ScreenToWorldPoint(touchPoint);
            aimDir = new Vector3(aimDir.x,aimDir.y,0);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                tempPoint.Clear();
                tempPoint.Add(touchPoint);
                armMotion.SetBool("ArmMotion",true);
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                isTouch = true;
                Aim();
            }
            if (Input.GetMouseButtonUp(0)&&isTouch)
            {
                tempPoint.Add(touchPoint);
                ChangShotSpeed();
                armMotion.SetBool("ArmMotion",false);
                Shot();
                print("all rise shot");
                isTouch = false;
            }
        }
	}
    public void Aim(int args) {
        arm.transform.LookAt(-aimDir);
    }
    public void Aim() {
        arm.transform.LookAt(aimDir);
    }
}
