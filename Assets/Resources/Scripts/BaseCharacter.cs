using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターの基底クラス
//これをアタッチすると以下の行動ができるようになる
//「移動」「ジャンプ」
//AIにも対応させるので、ここにはインターフェースの処理を書かないこと
public class BaseCharacter : EatBase
{
    protected float searchTimer = 0;//サーチ間隔
    protected float searchHeight = 0;//サーチ距離
    protected float searchRange = 0;//サーチ範囲
    protected bool isFowardHit;
    protected List<GameObject> SearchObjects;
    protected enum STATUS
    {
        WAIT = 0, WALK, DEATH, CATCH, THROW_IN,THROW_OUT, EAT
    };
    protected STATUS myStatus;

    public static GameObject CreateCharacter(string path,Vector3 pos = new Vector3())
    {
        GameObject g;
        try
        {
            //Debug.Log("pathname=" + path);
            string temp = path.Replace('\r', '\0');
            g = Instantiate(Resources.Load("Models/" + temp, typeof(GameObject))) as GameObject;
        }
        catch
        {
            Debug.Log("notpath=" + path);
            //オブジェクトパスが見つからない場合
            g = Instantiate(Resources.Load("Models/OUT_BOX", typeof(GameObject))) as GameObject;
          }
        g.transform.position = pos;

        g.gameObject.layer = 8;

        return g;
    }
    public void Initialize()
    {
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().center = new Vector3(0, 0.5f, 0);
        searchHeight = 200.0f;
        searchRange = 45.0f;
        SetSpeed(0.01f);
        //SetSpeed(transform.localScale.magnitude);
        MyDirection = transform.forward;

        HP = 300;
        //食べられたポイント
        eatPoint = 3;

        transform.tag = "Object";

        gameObject.layer = 8;

        //オブジェクトの追加
        ObjectManager.AddObject(gameObject);

    }
    public void Initialize(Vector3 pos, float speed)
    {
        MyPosition = pos;
        MySpeed = speed;
        isFowardHit = false;
        searchRange = 45.0f;
    }

    //Use this for initialization
    public virtual void Start()
    {
        MySpeed = 5.0f;
    }
    // Update is called once per frame
    public virtual void Update()
    {
            Move();
        //CheckGround();
    }
  
    public void MoveRight()
    {
        TargetPosition.x += 1.0f;
    }
    public void MoveLeft()
    {
        TargetPosition.x -= 1.0f;
    }
    public void MoveFront()
    {
        TargetPosition.z += 1.0f;
    }
    public void MoveBack()
    {
        TargetPosition.z -= 1.0f;
    }
    public void Move()
    {
        if (transform.parent != null)
        {
            return;
        }
        int hit = (isFowardHit) ? 0 : 1;
       Vector3 moving = TargetPosition - MyPosition;
        if (Math.Length(moving) <= 0.05f)
        {
            GetComponent<Animator>().SetFloat("walk", 0.0f);
            return;
        }
        else
        {
            GetComponent<Animator>().SetFloat("walk", 1.0f);
        }

        moving.y = 0;
       moving.Normalize();

        MyPosition += moving * MySpeed * Time.deltaTime*hit;

        MyPosition.y = transform.position.y;

        transform.position = MyPosition;

        SetDirection(moving);
        if (Math.Length(moving) >= 1.0f)
        {
            Quaternion q = Quaternion.LookRotation(moving);
            transform.rotation = q;
        }
        TargetPosition = MyPosition;
    }

    void Damage(int p)
    {
        HP -= p;
    }

    void Death()
    {
        HP = 0;
        Destroy(gameObject);
    }

    void LiveCheck()
    {
        if (HP <= 0)
            Death();
    }

    public void SetTarget(Vector3 target)
    {
        TargetPosition = target+MyPosition;
    }
    public Vector3 GetTarget()
    {
        return TargetPosition;
    }
    public void SetDirection(Vector3 v)
    {
        MyDirection = v;
    }

    //Y軸基点で回転(正数で左回転？)
    public void RotateY(float deg,float height=1)
    {
        Vector3 vector = (TargetPosition-MyPosition).normalized;
        //ラジアンに変換
        float rag = Math.DegToRag(deg);

        float ax = vector.x * Mathf.Cos(rag) - vector.z * Mathf.Sin(rag);
        float az = vector.x * Mathf.Sin(rag) + vector.z * Mathf.Cos(rag);

        vector.x = ax;
        vector.z = az;

       // SetDirection(vector+transform.position);
        SetTarget(vector*height);
    }

    public void SearchEnemy(GameObject obj)
    {
        //敵がいる時
        if (obj != null)
        {
            if (Math.Length(obj.transform.position - transform.position) <= 1.0f)
            {
                //Enemys.RemoveAt(gCount);
                Destroy(obj);
                obj = null;
                //timer = 0;
            }
        }
        else
        {
            //いない場合
            //timer += Time.deltaTime;
            //if (timer > 2)
            //{
            //    timer = 0;
            //    //RotateY(1, searchHeight);
            //    TargetPosition = HeadingCastle;
            //}
            int i = 0;
            float dis = 0;
            float ans = searchHeight;
            float temp_ans = searchHeight;

            List<GameObject> objects = new List<GameObject>();

            if (objects.Count > 0)
                foreach (GameObject g in objects)
                {

                    //探す計算処理
                    dis = Math.SearchCone(MyPosition, TargetPosition, searchHeight, searchRange, g.transform.position);
                    //視界に見えているもの
                    if (ans >= dis)
                    {
                        /*デバッグの色*/
                        g.GetComponent<Renderer>().material.color = Color.black;

                        //暫定的に一番近いものを検出し、無ければ最後に選んだものをターゲットにする
                        if (temp_ans >= dis)
                        {
                            temp_ans = dis;
                            //SetEnemy(i);
                        }
                    }
                    i++;
                }
        }
    }


    public List<GameObject> SearchObject(List<GameObject> objects, string tag = "untagged")
    {
        if (objects == null) return null;

        List<GameObject> objs = new List<GameObject>();
        float dis = 0;
        float ans = searchHeight;
        float temp_ans = searchHeight;

        isFowardHit = false;

        foreach (GameObject g in objects)
        {
            //同期中に削除されたものは見ない
            if (g != null)
            {
                if (g.tag == tag)
                {
                    //探す計算処理
                    dis = Math.Length(g.transform.position - transform.position);
                    //視界に見えているもの
                    if (ans >= dis)
                    {
                        //目の前にいるか調べる
                        if (Physics.Raycast(new Ray(transform.position, transform.forward),searchHeight/10))
                         {
                            isFowardHit = true;
                        }
                        //扇形に見る
                        if (Math.OnDirectionFan(MyPosition,MyDirection, g.transform.position,searchRange))
                        {
                            objs.Add(g);
                        }
                    }
                }
            }
        }
        SearchObjects = objs;
        Debug.DrawRay(MyPosition, MyDirection * searchHeight, Color.red, 0.3f);
        Debug.DrawRay(MyPosition, Math.getDirectionDegree(MyDirection, searchRange,searchHeight), Color.green, 0.3f);
        Debug.DrawRay(MyPosition, Math.getDirectionDegree(MyDirection, -searchRange,searchHeight), Color.green, 0.3f);
        return objs;
    }

    void OnCollisionEnter(Collision c)
    {

    }
    void OnCollisionStay(Collision c)
    {

    }
    void OnCollisionExit(Collision c)
    {

    }
    public void SetSpeed(float sp)
    {
        MySpeed = sp;
    }

    void CheckGround()
    {
   
    }
    public void SetMass(float s)
    {
       // rigid.mass = s;
    }
    protected void SetStatus(STATUS s)
    {
        myStatus = s;
    }
    public void UnderGround()
    {
        if (transform.position.y < -1)
        {
            transform.position = new Vector3(transform.position.x,1,transform.position.z);
        }
    }
}
