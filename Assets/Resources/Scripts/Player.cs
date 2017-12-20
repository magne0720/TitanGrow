﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 成長し続けるキャラクターのクラス
/// </summary>
public class Player : BaseCharacter
{
    public List<GameObject> catchObjects;                        //つかんだものリスト

    public int FoodPoint;                                       //食ったものポイント 
    public float CastAwaySpeed = 1250.0f;                   //投げた時のスピード
    public float GrowTime = 0.0f;                                //成長が止まっている時間
    public Vector3 GrowRate = new Vector3(0.001f, 0.001f, 0.001f);  //大きさの倍率

    private float actiontimer = 0;//待機モーションフラグ

    public static GameObject CreatePlayer(string path,Vector3 pos=new Vector3())
    {
        GameObject g;

        path.Replace('\r', '\0');
        try
        {
            g = Instantiate(Resources.Load("Models/" + path, typeof(GameObject))) as GameObject;
        }
        catch
        {
            //オブジェクトパスが見つからない場合
            g = Instantiate(Resources.Load("Models/DummyPre", typeof(GameObject))) as GameObject;
            Debug.Log("Object Null");
        }
        g.AddComponent<Player>();
        g.transform.position = pos;
        g.transform.name = "Player";
        g.transform.tag = "Player";

        return g;
    }
    // Use this for initialization
    override public void Start()
    {
        actiontimer = 0;
        Initialize();
        //GetComponent<Rigidbody>().freezeRotation = true;
        catchObjects = new List<GameObject>();
        this.transform.name = "Player";
        this.transform.tag = "Player";
        GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load("Models/bigmen", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
    }

    // Update is called once per frame
    override public void Update()
    {
        if (Math.Length(TargetPosition - MyPosition) < 0.1f)
        {
            actiontimer += Time.deltaTime;
            if (actiontimer >= 7.0f)
            {
                actiontimer = 0;
                GetComponent<Animator>().SetTrigger("waitaction");
            }
        }
        //MyPosition = transform.position;
        //成長の制御
        Grow(FoodPoint);
            Move();
        GetComponent<Animator>().SetFloat("walk",1.0f);

        //SetMass(transform.localScale.magnitude);
        //UnderGround();
        if (catchObjects.Count > 0)
        {
            foreach (GameObject g in catchObjects)
            {
                //g.GetComponent<Rigidbody>().isKinematic = false;
                g.transform.position = transform.forward * transform.localScale.z + transform.position;
            }
        }
    }

    //タグがFoodなら当たったものをCatchにもっていく
    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag != "Untagged")
        {
            collision.transform.GetComponent<Rigidbody>().AddForce(transform.forward * CastAwaySpeed * transform.localScale.z);
        }
        transform.GetComponent<Rigidbody>().AddForce(-transform.forward);

    }
    public void CatchAction()
    {
        if (catchObjects.Count > 0)
        {
                CastAway();          
        }
        else
        {
            SearchObject(ObjectManager.GameObjects, "Untagged");
            Catch(SearchObjects);
        }
    }

    //つかむ
    public void Catch(GameObject g)
    {
        //つかんだもの情報
        catchObjects.Add(g);
        //子にする
        //g.transform.parent = this.transform;
        //子のあたり判定を消す
        //g.GetComponent<Collider>().enabled = false;
    }
    //つかむ
    public void Catch(List<GameObject> objs)
    {
        catchObjects.Clear();
        //つかんだもの情報
        if (objs.Count > 0)
            foreach (GameObject g in objs)
            {
                if (g.transform.localScale.magnitude/2 < transform.localScale.magnitude)
                    if (g.tag != "Player"&&g.tag!="Castle")
                    {
                        catchObjects.Add(g);
                        //子にする
                        g.transform.parent = this.transform;
                        //子のあたり判定を消す
                        //g.GetComponent<Collider>().enabled = false;
                        //デバッグ用
                        //g.GetComponent<Renderer>().material.color = Color.black;
                        // アニメーターで動かす
                        GetComponent<Animator>().SetTrigger("catch");
                    }
            }
    }

    //投げる
    public void CastAway()
    {
        foreach (GameObject g in catchObjects)
        {
            //つかんだものの衝突判定を戻す
            //g.GetComponent<Collider>().enabled = true;
            //自身のz軸方面に飛ばす
            g.GetComponent<EatBase>().AddForce(transform.forward+new Vector3(0,100,0),transform.localScale.z);
            //子から話す
            g.transform.parent = null;
         }
        //リストの初期化
        catchObjects.Clear();
        // アニメーターで動かす
        GetComponent<Animator>().SetTrigger("castaway");
    }

    //離す
    public void Release()
    {
        foreach (GameObject g in catchObjects)
        {
            //子のあたり判定を戻す
            g.GetComponent<Collider>().enabled = true;
            g.transform.parent = null;
        }
        //リストの初期化
        catchObjects.Clear();
        // アニメーターで動かす
        GetComponent<Animator>().SetTrigger("release");
    }

    //食べる
    public void Eat()
    {
        List<GameObject> eats=new List<GameObject>();

        foreach (GameObject g in catchObjects)
        {
            //ポイントを得る処理
            EatPoint(g.GetComponent<EatBase>());

            //食べたものを消す処理
            ObjectManager.removeObject(g);
            Destroy(g);
            
        }
        //リストの初期化
        catchObjects.Clear();
        // アニメーターで動かす
        GetComponent<Animator>().SetTrigger("eat");
    }

    //食べた時のポイントを追加する
    public void EatPoint(EatBase e)
    {
        FoodPoint += e.eatPoint;
    }
    //自身の成長
    public void Grow(int point)
    {
        if (FoodPoint > 0)
        {
            GrowTime += Time.deltaTime;
            if (GrowTime >= 1.0f)
            {
                GrowTime = 0;
                FoodPoint--;
            }
            return;
        }
        else
        {
            //大きくなる
            transform.localScale += GrowRate;

            searchHeight = 3.0f * transform.localScale.z;

            //大きさに比例してスピードが上がる
            SetSpeed(transform.localScale.magnitude);
        }
    }

    public void GlowCount(int point)
    {
       
    }
}
