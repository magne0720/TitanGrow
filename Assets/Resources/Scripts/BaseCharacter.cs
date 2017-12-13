using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターの基底クラス
//これをアタッチすると以下の行動ができるようになる
//「移動」「ジャンプ」
//AIにも対応させるので、ここにはインターフェースの処理を書かないこと
public class BaseCharacter : EatBase
{
    public int HP;
    public float MySpeed;
    public Vector3 MyPosition;
    public Vector3 TargetPosition;
    public Vector3 MyDirection;     //自身の向いている方向    
    public float serchHeight = 0;//サーチ距離
    public float serchRange = 0;//サーチ範囲
    public bool isFowardHit;
    public List<GameObject> SerchObjects;

    public static GameObject CreateCharacter(string path,Vector3 pos = new Vector3())
    {
        string temp=path.Substring(0, 7);
        GameObject g;
        try
        {
            g = Instantiate(Resources.Load("Models/" + temp, typeof(GameObject))) as GameObject;
        }
        catch
        {
            //オブジェクトパスが見つからない場合
                g = Instantiate(Resources.Load("Models/OUT_BOX", typeof(GameObject))) as GameObject;
          }
        g.transform.position = pos;

        return g;
    }
    public void Initialize()
    {
        //if (rigid==null)
        //{
        //    rigid = gameObject.AddComponent<Rigidbody>();
        //    gameObject.AddComponent<CapsuleCollider>().radius = 0.1f;
        //    gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0, 0.5f, 0);
        //    //rigid.freezeRotation = true;
        //}
        serchHeight = 200.0f;
        serchRange = 22.5f;
        SetSpeed(0.01f);
        //SetSpeed(transform.localScale.magnitude);
        MyDirection = transform.forward;

        //食べられたポイント
        eatPoint = 200;


        //オブジェクトの追加
        ObjectManager.AddObject(gameObject);

    }
    public void Initialize(Vector3 pos, float speed)
    {
        MyPosition = pos;
        MySpeed = speed;
        isFowardHit = false;
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
       if (Math.Length(moving) <= 0.002f) return;
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

    public void SerchEnemy(GameObject obj)
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
            //    //RotateY(1, serchHeight);
            //    TargetPosition = HeadingCastle;
            //}
            int i = 0;
            float dis = 0;
            float ans = serchHeight;
            float temp_ans = serchHeight;

            List<GameObject> objects = new List<GameObject>();

            if (objects.Count > 0)
                foreach (GameObject g in objects)
                {

                    //探す計算処理
                    dis = Math.SerchCone(MyPosition, TargetPosition, serchHeight, serchRange, g.transform.position);
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


    public List<GameObject> SerchObject(List<GameObject> objects, string tag = "Enemy")
    {
        if (objects == null) return null;

        List<GameObject> objs = new List<GameObject>();
        float dis = 0;
        float ans = serchHeight;
        float temp_ans = serchHeight;

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
                        if (Physics.Raycast(new Ray(transform.position, transform.forward),serchHeight/10))
                         {
                            Debug.Log("out");
                            isFowardHit = true;
                        }
                        //扇形に見る
                        if (Math.OnDirectionFan(MyPosition,MyDirection, g.transform.position,serchRange))
                        {
                            objs.Add(g);
                        }
                    }
                }
            }
        }
        SerchObjects = objs;
        Debug.DrawRay(MyPosition, MyDirection * serchHeight, Color.red, 0.3f);
        Debug.DrawRay(MyPosition, Math.getDirectionDegree(MyDirection, serchRange,serchHeight), Color.green, 0.3f);
        Debug.DrawRay(MyPosition, Math.getDirectionDegree(MyDirection, -serchRange,serchHeight), Color.green, 0.3f);
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
    public void UnderGround()
    {
        if (transform.position.y < -1)
        {
            transform.position = new Vector3(transform.position.x,1,transform.position.z);
        }
    }
}
