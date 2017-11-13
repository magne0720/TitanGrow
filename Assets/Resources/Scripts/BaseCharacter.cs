﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターの基底クラス
//これをアタッチすると以下の行動ができるようになる
//「移動」「ジャンプ」
//AIにも対応させるので、ここにはインターフェースの処理を書かないこと
public class BaseCharacter : MonoBehaviour
{
    public int MyHitpoint;
    public float MySpeed;
    public GameObject MyModel=null;
    public Vector3 MyPosition;
    public Vector3 TargetPosition;
    public Vector3 MyDirection;     //自身の向いている方向    
    public float serchHeight = 0;//サーチ距離
    public float serchRange = 0;//サーチ範囲
    public bool isGround;
    public CharacterController c;

    public static GameObject CreateCharacter(string path)
    {
        GameObject baseCharacter;

        if (path == "a")
        {
            baseCharacter = Instantiate(Resources.Load("Prefabs/test", typeof(GameObject)))as GameObject;
        }
        else
        {
            baseCharacter = Resources.Load(path) as GameObject;
        }
        return baseCharacter;
    }

    //Use this for initialization
    public void Start()
    {
        if (MyModel == null)
        {
            MyModel = this.gameObject;
        }

        MySpeed = 5.0f;
    }
    // Update is called once per frame
    public void Update()
    {
        Move();
        //CheckGround();
        if (isGround)
        {

        }
        //LiveCheck();
    }
    public void Initialize(Vector3 pos, float speed)
    {
        MyPosition = pos;
        MySpeed = speed;
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
        Vector3 moving = TargetPosition - MyPosition;
        if (Math.Length(moving) <= MySpeed*Time.deltaTime) return;
       moving.Normalize();

        MyPosition += moving * MySpeed * Time.deltaTime;

        transform.position = MyPosition;

        //SetDirection(moving);
        Quaternion q = Quaternion.LookRotation(moving);
        transform.rotation = q;

    }

    void Damage(int p)
    {
        MyHitpoint -= p;
    }

    void Death()
    {
        MyHitpoint = 0;
        Destroy(gameObject);
    }

    void LiveCheck()
    {
        if (MyHitpoint <= 0)
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
                    g.GetComponent<Renderer>().material.color = Color.white;
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
}
