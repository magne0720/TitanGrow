using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{
    private float z;                                             //自身のZ軸

    public List<GameObject> catchObjects;                        //つかんだものリスト

    public int FoodPoint;                                        //食ったものポイント 
    public float CastAwaySpeed = 1250.0f;                        //投げた時のスピード
    public float GrowTime = 0.0f;                                //成長が止まっている時間　
    public bool Growflag = true;                                 //成長しているかどうか
    public Vector3 GrowRate = new Vector3(0.05f, 0.05f, 0.05f);  //大きさの倍率

    // Use this for initialization
    void Start()
    {
        this.transform.tag = "Player";
        base.Start();
        z = transform.position.z; //自身のZ軸を取得
    }

    // Update is called once per frame
    void Update()
    {

        //投げる
        if (Input.GetKeyDown(KeyCode.U))
        {
            CastAway();
        }

        //離す
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Release();
        }

        //食べる
        if (Input.GetKeyDown(KeyCode.E))
        {
            Eat();
        }

        //大きくなる
        if (Growflag == true)
        {
            Grow(FoodPoint);
        }

        //4秒の間成長が止まる
        if (Growflag == false)
        {
            GrowTime += Time.deltaTime;
            if (GrowTime >= FoodPoint)
            {
                //成長再開
                Growflag = true;
                GrowTime = 0.0f;
                //食ったもののポイントをなくす
                FoodPoint = 0;
            }
        }


        Move();

    }

    //タグがFoodなら当たったものをCatchにもっていく
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Catch(collision.transform.gameObject);
        }
    }

    //つかむ
    void Catch(GameObject g)
    {
        //つかんだもの情報
        catchObjects.Add(g);
        //子にする
        g.transform.parent = this.transform;
        //子のあたり判定を消す
        g.GetComponent<Collider>().enabled = false;
    }

    //投げる
    void CastAway()
    {
        foreach (GameObject g in catchObjects)
        {
            //このあたり判定を戻す
            g.GetComponent<Collider>().enabled = true;
            //自身のz軸方面に飛ばす
            g.GetComponent<Rigidbody>().AddForce(transform.forward * CastAwaySpeed);
            g.transform.parent = null;
        }

        //リストの初期化
        catchObjects.Clear();


    }

    //離す
    void Release()
    {
        foreach (GameObject g in catchObjects)
        {
            //子のあたり判定を戻す
            g.GetComponent<Collider>().enabled = true;
            g.transform.parent = null;
        }
        //リストの初期化
        catchObjects.Clear();

    }

    //食べる
    void Eat()
    {
        foreach (GameObject g in catchObjects)
        {
            //ポイントを得る処理
            EatPoint(g, FoodPoint);

            //食べたものを消す処理
            DestroyObject(g);

        }
        //リストの初期化
        catchObjects.Clear();

    }

    //食べた時のポイントを追加する
    void EatPoint(GameObject g, int point)
    {

        //Ainimalが含まれている文字列
        if (g.name.IndexOf("Animal") >= 0)
        {
            FoodPoint += 4;
            GlowCount(FoodPoint);

        }
        //Humanが含まれている文字列
        if (g.name.IndexOf("Human") >= 0)
        {
            FoodPoint += 2;
            GlowCount(FoodPoint);
        }

    }

    //自身の成長
    void Grow(int point)
    {
        //大きくなる
        transform.localScale += GrowRate;
    }

    void GlowCount(int point)
    {
        //大きくなるのを止める
        if (FoodPoint >= 1)
        {
            Growflag = false;
        }
    }

}
