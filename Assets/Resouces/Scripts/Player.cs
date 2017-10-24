using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter {
    public int hp;                  //自身の体力
    public GameObject catchObject;  //つかんだもの
    float z;
   



    // Use this for initialization
    void Start () {
        z = transform.position.z;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            CastAway(catchObject);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Release(catchObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            catchObject = collision.transform.gameObject;
            Catch(catchObject);
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
        catchObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f);
        //catchObject.GetComponent<Rigidbody>().AddForce(0,1000.0f, 1000.0f);
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
        EatPoint(catchObject);

        //食べたものを消す処理
        DestroyObject(g);
    }

    //食べた時のポイントを追加する
    void EatPoint(GameObject g)
    {

    }

    //自身の成長
    void Grow()
    {

    }

    //ダメージを受ける
    void Damege(int point)
    {

    }
}
