using System.Collections;
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
    public Vector3 GrowRate = new Vector3(0.05f, 0.05f, 0.05f);  //大きさの倍率

    public static GameObject Create(string path)
    {
        GameObject g;

        if (path == "a")
        {
            g = Instantiate(Resources.Load("Prefabs/test", typeof(GameObject))) as GameObject;
        }
        else
        {
            g = Resources.Load(path) as GameObject;
        }
        return g;
    }
    // Use this for initialization
    override public void Start()
    {
        Initialize();
        this.transform.name = "Player";
        this.transform.tag = "Player";
        //GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    override public void Update()
    {
        //つかむ
        if (Input.GetKey(KeyCode.Alpha0))
        {
            serchHeight = 100.0f;
            serchRange = 120.0f;
            Catch(SerchObject(ObjectManager.GameObjects));
        }

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

        Move();
        //成長の制御
        Grow(FoodPoint);
        SetMass(transform.localScale.magnitude);
        UnderGround();
    }

    //タグがFoodなら当たったものをCatchにもっていく
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Catch(collision.transform.gameObject);
        }
    }
    public void CatchAction()
    {
        if (catchObjects.Count > 0)
        {
            Release();
        }
        else
        {
            //Catch(new GameObject());
        }
    }

    //つかむ
    public void Catch(GameObject g)
    {
        //つかんだもの情報
        catchObjects.Add(g);
        //子にする
        g.transform.parent = this.transform;
        //子のあたり判定を消す
        g.GetComponent<Collider>().enabled = false;
    }
    //つかむ
    public void Catch(List<GameObject> objs)
    {
        //つかんだもの情報
        if (objs.Count > 0)
            foreach (GameObject g in objs)
            {
                catchObjects.Add(g);
                //子にする
                g.transform.parent = this.transform;
                //子のあたり判定を消す
                g.GetComponent<Collider>().enabled = false;
            }
    }

    //投げる
    public void CastAway()
    {
        foreach (GameObject g in catchObjects)
        {
            //つかんだものの衝突判定を戻す
            g.GetComponent<Collider>().enabled = true;
            //自身のz軸方面に飛ばす
            g.GetComponent<Rigidbody>().AddForce(transform.forward * CastAwaySpeed);
            g.transform.parent = null;
        }
        //リストの初期化
        catchObjects.Clear();
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
    }

    //食べる
    public void Eat()
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
    public void EatPoint(GameObject g, int point)
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
            transform.localScale -= GrowRate;
        }

        //大きくなる
        transform.localScale += GrowRate;

        //大きさに比例してスピードが上がる
        SetSpeed(transform.localScale.magnitude);
    }

    public void GlowCount(int point)
    {
       
    }
}
