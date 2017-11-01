using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter {
    public int hp;                  //自身の体力
    public GameObject catchObject;  //つかんだもの
    private float z;                //自身のZ軸
    public int FoodPoint;           //食ったものポイント 
    private float timeElapsed;      //時間



    // Use this for initialization
    void Start () {
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
        Grow(FoodPoint); 
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
        catchObject.GetComponent<Rigidbody>().AddForce(transform.forward * 750.0f);
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
        if(catchObject.name == "Capsule")
        {

            FoodPoint -= 4;
        }

        if (catchObject.name == "Cube")
        {
            FoodPoint -= 2;
        }


        if (FoodPoint == 6)
        {
            transform.localScale += new Vector3(-2.0f, -2.0f, -2.0f);
        }
    }

    //自身の成長
    void Grow(int point)
    {
        //大きくなる
        transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
    }

    //ダメージを受ける
    void Damege(int point)
    {
        hp -= 1;
    }
}
