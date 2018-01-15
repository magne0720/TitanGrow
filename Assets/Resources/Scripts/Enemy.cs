using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを阻むキャラクターの基底クラス
/// 指定した場所に移動する
/// プレイヤーを見つけ次第追いかけてくる
/// </summary>
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
    
    private float timer = 0;

    public static GameObject CreateEnemy(string path="a")
    {
        GameObject g = CreateCharacter(path);
        
        g.name = path;
        g.AddComponent<Enemy>();


        return g;
    } 
    public static GameObject CreateEnemy(DataBaseManager.OBJECT data)
    {
        GameObject g = CreateCharacter(data.path);
        g.name = data.name;
        g.transform.position = data.pos;
        g.transform.localScale *= data.scale;
        g.AddComponent<Enemy>();

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
        MySpeed = 4.0f;
        HP = 15;
        if(lastTarget==Vector3.zero)
        lastTarget = new Vector3(transform.position.x, transform.position.y, 2);
        timer = 0;
        HeadingCastle = DataBaseManager.GetHumanCastle();
        MyPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (HP < 0)
        {
            Destroy(gameObject);
        }
        if (searchTimer < 1.0f)
        {
            searchTimer += Time.deltaTime;
        }
        else
        {
            HP--;
            ActionBrain();
            searchTimer = 0;
        }


        if (battleEnemy != null)
        {
            SetTarget(battleEnemy.transform.position - transform.position);
        }
        else
        {
            SetTarget(new Vector3(0,0,-500));
        }
        Move();
        MyPosition = transform.position;

        //UnderGround();
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
            SearchObject(ObjectManager.GameObjects,"Player");
            foreach(GameObject g in SearchObjects)
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
