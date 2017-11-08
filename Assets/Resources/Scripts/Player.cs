using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter {
    public int hp;                                               //自身の体力
    private float z;                                             //自身のZ軸

    public GameObject catchObject;                               //つかんだもの

    public int FoodPoint;                                        //食ったものポイント 
    public float CastAwaySpeed = 1250.0f;                        //投げた時のスピード
    public float GrowTime = 0.0f;                                //　
    public bool flag = true;                                     //

    public Vector3 GrowRate = new Vector3(0.05f, 0.05f, 0.05f);  //大きさの倍率

    // Use this for initialization
    void Start () {
        this.transform.tag = "Player";
        base.Start();
        z = transform.position.z; //自身のZ軸を取得

    }
	
	// Update is called once per frame
	void Update () {
        //投げる
        if (Input.GetKeyDown(KeyCode.U))
        {
            CastAway(catchObject);
        }

        //離す
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Release(catchObject);
        }

        //食べる
        if (Input.GetKeyDown(KeyCode.E))
        {
            Eat(catchObject);
        }


        //大きくなる
        if (flag == true)
        {
            Grow(FoodPoint);
        }

        //4秒の間成長が止まる
        if (flag == false)
        {
            GrowTime += Time.deltaTime;
            if(GrowTime >= 4.0f)
            {
                //成長再開
                flag = true;
                GrowTime = 0.0f;
            }
        }
        Move();

    }

    void OnCollisionEnter(Collision collision)
    {
            if (catchObject == null)
            {
                if (collision.gameObject.tag == "Food")
                {
                    catchObject = collision.transform.gameObject;
                    Catch(catchObject);
                }
            }
    }

    //つかむ
    void Catch(GameObject g)
    {
        catchObject = g;

        g.transform.parent = this.transform;
    }

    //投げる
    void CastAway(GameObject g)
    {
        //自身のz軸方面に飛ばす
        catchObject.GetComponent<Rigidbody>().AddForce(transform.forward * CastAwaySpeed);
        g.transform.parent = null;
        catchObject = null;
    }

    //離す
    void Release(GameObject g)
    {
        g.transform.parent = null;
        catchObject = null;
    }

    //食べる
    void Eat(GameObject g)
    {
        //ポイントを得る処理
        EatPoint(catchObject, FoodPoint);

        //食べたものを消す処理
        DestroyObject(g);
    }

    //食べた時のポイントを追加する
    void EatPoint(GameObject g, int point)
    {

        //Ainimal含まれている
        if(catchObject.name.IndexOf("Animal") >= 0 )
        {
            FoodPoint = 4;
            GlowCount(FoodPoint);

        }
        //Human
        if (catchObject.name.IndexOf("Human") >= 0)
        {
            FoodPoint -= 2;
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
        if(FoodPoint == 4)
        {
            flag = false;
        }
        ////大きくなるのを再開
        //if (GrowTime >= 4.0f)
        //{
        //}

    }

    //ダメージを受ける
    void Damege(int point)
    {
        hp -= point;
    }
}
