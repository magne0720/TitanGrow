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

    public static GameObject CreateEnemy(string path="Prefabs/test")
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
        g.name = path;
        g.AddComponent<Enemy>();

        //オブジェクトの追加
        ObjectManager.AddObject(g);

        return g;
    } 
    public static GameObject CreateEnemy(DataBaseManager.ENEMY data)
    {
        GameObject g;
            g = Instantiate(Resources.Load("Models/"+data.path, typeof(GameObject))) as GameObject;    
        g.name = data.path;
        g.transform.position = data.pos;
        g.AddComponent<Enemy>();
        
        //オブジェクトの追加
        ObjectManager.AddObject(g);

        return g;
    }
    static Enemy Create(string path, Vector3 lastPos)
    {
        Enemy e = new Enemy();
        
        e.lastTarget = lastPos;


        return e;
    }
    // Use this for initialization
    void Start()
    {
        Initialize();
        if(lastTarget==Vector3.zero)
        lastTarget = new Vector3(transform.position.x, transform.position.y, 2);
        timer = 0;
        HeadingCastle = new Vector3(40, 0,40);
        MyPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        ActionBrain();
        if(battleEnemy!=null)
        SetTarget(battleEnemy.transform.position - transform.position);
      
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
    void ActionBrain()
    {
        if (battleEnemy == null)
        {
            SerchObject(ObjectManager.GameObjects,"Player");
            foreach(GameObject g in SerchObjects)
            {
                if (g.tag == "Player")
                {
                    SetEnemy(g);
                }
            }
        }
        else
        {

        }
    }
}
