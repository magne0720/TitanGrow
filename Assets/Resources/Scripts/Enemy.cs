using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{

    public float eyeRange;//視覚距離
    //性格
    public enum EMOTION
    {
        NOTHING, JOY, SAD, GLAD, ANGER
    };
    public EMOTION myEmotion;//自身の感情
    public float attackDelay;//攻撃速度

    public Vector3 lastTarget;//敵の城の拠点
    public int battleEnemyCount;//今から戦いに行く敵のカウント
    public GameObject battleEnemy;//今から戦いに行く敵
    public Vector3 HeadingCastle;//向かう城
    public List<GameObject> Enemys;
    
    public GameObject testObject = null;//テスト用オブジェクト
    private float timer = 0;

    public static GameObject Create(string path="Prefabs/test")
    {
        GameObject g;
        if (path == "a")
        {
             g = Instantiate(Resources.Load("Prefabs/test", typeof(GameObject))) as GameObject;
        }
        else
        {
             g = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
        }

        return g;
    } 
    
    static Enemy Create(string path, Vector3 lastPos)
    {
        Enemy e = new Enemy();

        //e.MyModel = Resources.Load(path) as GameObject;
        e.lastTarget = lastPos;

        return e;
    }

    // Use this for initialization
    void Start()
    {
        Initialize();
        if(lastTarget==Vector3.zero)
        lastTarget = new Vector3(transform.position.x, transform.position.y, 2);
        //MyPosition = transform.position;
        //SetTarget(lastTarget);
        //serchHeight = 25.0f;
        //serchRange = 120.0f;
        timer = 0;
        HeadingCastle = new Vector3(40, 0,40);
        MySpeed = transform.localScale.z*0.2f;
        //GetComponent<Rigidbody>().isKinematic = true;
        MyPosition = transform.position;

        SetTarget(battleEnemy.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        SetTarget(battleEnemy.transform.position - MyPosition);

        SetMass(transform.localScale.magnitude);

        Move();
        UnderGround();
    }
    //襲う敵の設定
    void SetEnemy(int i)
    {
        battleEnemyCount = i;
        battleEnemy = Enemys[i];
        //SetTarget(Enemys[i].transform.position);
    }
    //敵を探す

    public void SetEnemy(GameObject g)
    {
        battleEnemy = g;
        //SetTarget(Enemys[i].transform.position);
    }


    private void SetSerchHeight(float d)
    {
        //serchHeight = d;
    }
    private void SetSerchRange(float d)
    {
        //serchRange = d;
    }
}
