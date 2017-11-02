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
    public float serchHeight = 0;//サーチ距離
    public float serchRange = 0;//サーチ範囲
    public Vector3 lastTarget;//敵の城の拠点
    public int battleEnemyCount;//今から戦いに行く敵のカウント
    public GameObject battleEnemy;//今から戦いに行く敵
    public List<GameObject> Enemys;//自身から見て敵となるもの


    public GameObject testObject = null;//テスト用オブジェクト
    private float timer = 0;

    public static GameObject Create(GameObject g)
    {
        if (g.GetComponent<Enemy>()==null)
        g.AddComponent<Enemy>();

        return g;
    } 

    static Enemy Create(string path)
    {
        Enemy e = new Enemy();

        e.MyModel = Resources.Load(path) as GameObject;

        return e;
    }
    static Enemy Create(string path, Vector3 lastPos)
    {
        Enemy e = new Enemy();

        e.MyModel = Resources.Load(path) as GameObject;
        e.lastTarget = lastPos;

        return e;
    }

    // Use this for initialization
    void Start()
    {
        MySpeed = 1.0f;
        if(lastTarget==Vector3.zero)
        lastTarget = new Vector3(transform.position.x, transform.position.y, 2);
        MyPosition = transform.position;
        SetTarget(lastTarget);
        serchHeight = 5.0f;
        serchRange = 30.0f;
        timer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        serchEnemy();
        
        if (Input.GetKey(KeyCode.K))
        {
            GameObject g = Instantiate(testObject);
            g.transform.position = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3));
            Enemys.Add(g);
        }

        if (Input.GetKeyDown(KeyCode.L))
            debugPoint();

        //if (Input.GetKey(KeyCode.Comma))
        //{
        //    transform.Rotate(new Vector3(0, -5, 0));
        //    SetDirection();
        //}
        //if (Input.GetKey(KeyCode.Period))
        //{
        //    transform.Rotate(new Vector3(0, 5, 0));
        //    SetDirection();
        //}

    }
    //襲う敵の設定
    void SetEnemy(int i)
    {
        battleEnemyCount = i;
        battleEnemy = Enemys[i];
        SetTarget(Enemys[i].transform.position);
    }
    //敵を探す
    void serchEnemy()
    {
        //敵がいる時
        if (battleEnemy != null)
        {
            if (Math.Length(battleEnemy.transform.position - transform.position)<=1.0f)
            {
                Enemys.RemoveAt(battleEnemyCount);
                Destroy(battleEnemy);
                battleEnemy = null;
            }
           // return;
        }

        int i = 0;
        float dis = 0;
        float ans = serchHeight;
        float temp_ans = serchHeight;

        foreach (GameObject g in Enemys)
        {
            //探す計算処理
            dis = Math.SerchCone(MyPosition,TargetPosition, serchHeight,serchRange, g.transform.position);
            //視界に見えているもの
            if (ans >= dis)
            {
                g.transform.Rotate(new Vector3(8, 0, 0));
                //暫定的に一番近いものを検出し、無ければ最後に選んだものをターゲットにする
                if (temp_ans >= dis)
                {
                    temp_ans = dis;
                    //SetEnemy(i);
                }
            }
            i++;
        }


        if (battleEnemy == null)
        {
            timer += 1;
            if (timer > 120)
            {
                timer = 0;
                TargetPosition += new Vector3(0, 0, 2);
            }
        }
    }

    private void debugPoint()
    {
        for (int x = -5; x < 5; x++)
            for (int y = -5; y < 5; y++)
            {
                GameObject g = Instantiate(testObject);
                g.transform.position = new Vector3(x, 0, y);
                Enemys.Add(g);
            }
    }
}
