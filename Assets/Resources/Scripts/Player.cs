using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 成長し続けるキャラクターのクラス
/// </summary>
public class Player : BaseCharacter
{
    public List<GameObject> catchObjects;                        //つかんだものリスト

    private int FoodPoint;                                       //食ったものポイント 
    private float CastAwaySpeed = 1250.0f;                   //投げた時のスピード
    private float GrowTime = 0.0f;                                //成長が止まっている時間
    private Vector3 GrowRate = new Vector3(0.001f, 0.001f, 0.001f);  //大きさの倍率

    private float actiontimer = 0;//待機モーションフラグ

    private const float TIME_ACTION_CATCH = 1.0f;
    private const float TIME_ACTION_THROW_IN = 1.0f;
    private const float TIME_ACTION_THROW_OUT = 1.0f;
    private const float TIME_ACTION_EAT = 1.0f;

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
        return g;
    }
    // Use this for initialization
    override public void Start()
    {

        catchObjects = new List<GameObject>();
        SetStatus(STATUS.WAIT);
        actiontimer = 0;
        Initialize();
        //GetComponent<Rigidbody>().freezeRotation = true;
        transform.name = "Player";
        transform.tag = "Player";
        GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load("Models/bigmen", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;

        gameObject.layer = 8;

        try
        {
            foreach (GameObject g in gameObject.GetComponentsInChildren<GameObject>())
            {
                g.gameObject.layer = 8;
            }
        }
        catch { }


        transform.localScale /= 100.0f;
    }

    // Update is called once per frame
    override public void Update()
    {
        searchTimer += Time.deltaTime;
        if (searchTimer >= 0.1f)
        {
            SearchObject(ObjectManager.GameObjects, "Object");
            searchTimer = 0.0f;
        }
        if (HP < 0)
        {
            Debug.Log("DEATH");
            SetStatus(STATUS.DEATH);
        }
        actiontimer += Time.deltaTime;
        switch (myStatus)
        {
            case STATUS.WAIT:
                actiontimer = 0;
                break;
            case STATUS.WALK:
                actiontimer = 0;
                break;
            case STATUS.DEATH:
                if (actiontimer > 5.0f)
                {
                    Debug.Log("<color=red>STATUS-DEATH</color>");
                }
                break;
            case STATUS.CATCH:
                if (actiontimer > TIME_ACTION_CATCH)
                {
                    Debug.Log("<color=red>STATUS-CATCH</color>");
                    SetStatus(STATUS.WAIT);
                }
                break;
            case STATUS.THROW_IN:
                if (actiontimer > TIME_ACTION_THROW_IN)
                {
                    Debug.Log("<color=red>STATUS-THROW_IN</color>");
                    SetStatus(STATUS.WAIT);
                }
                break;
            case STATUS.THROW_OUT:
                if (actiontimer > TIME_ACTION_THROW_OUT)
                {
                    Debug.Log("<color=red>STATUS-THROW_OUT</color>");
                    SetStatus(STATUS.WAIT);
                }
                break;
            case STATUS.EAT:
                TargetPosition = Vector3.zero;
                if (actiontimer > TIME_ACTION_EAT)
                {
                    Debug.Log("<color=red>STATUS-EAT</color>");
                    SetStatus(STATUS.WAIT);
                }
                break;
            default:
                break;
        };


        ////屈伸アクション
        //if (Math.Length(TargetPosition - MyPosition) < 0.1f)
        //{
        //    actiontimer += Time.deltaTime;
        //    if (actiontimer >= 7.0f)
        //    {
        //        actiontimer = 0;
        //        GetComponent<Animator>().SetTrigger("waitaction");
        //    }
        //}
        //MyPosition = transform.position;
        //成長の制御
        Grow(FoodPoint);

        if (myStatus == STATUS.WAIT || myStatus == STATUS.WALK)
            Move();
      
        //foreach(GameObject g in SearchObjects)
       // {
            //g.GetComponent<Renderer>().material.color = Color.red;
        //}
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

    public void CatchAction()
    {
        if (catchObjects.Count > 0)
        {
                CastAway();          
        }
        else
        {
            
            Catch(SearchObjects);
        }
    }

    //つかむ
    public void Catch(GameObject g)
    {
        SetStatus(STATUS.CATCH);

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
        SetStatus(STATUS.CATCH);

        catchObjects.Clear();
        //つかんだもの情報
        if (objs.Count > 0)
            foreach (GameObject g in objs)
            {
                if (g.transform.localScale.y/3< transform.localScale.y*2)
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
        SetStatus(STATUS.THROW_IN);

        foreach (GameObject g in catchObjects)
        {
            //つかんだものの衝突判定を戻す
            //g.GetComponent<Collider>().enabled = true;
            //自身のz軸方面に飛ばす
            g.GetComponent<EatBase>().AddForce(transform.forward,transform.localScale.z);
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
        SetStatus(STATUS.THROW_OUT);

        foreach (GameObject g in catchObjects)
        {
            //子のあたり判定を戻す
            g.GetComponent<Collider>().enabled = true;
            g.transform.parent = null;
            //g.GetComponent<Renderer>().material.color = Color.white;
        }
        //リストの初期化
        catchObjects.Clear();
        // アニメーターで動かす
        GetComponent<Animator>().SetTrigger("release");
    }

    //食べる
    public void Eat()
    {
        SetStatus(STATUS.EAT);

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
        FoodPoint += e.GetEatPoint();
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
            SetSpeed(transform.localScale.magnitude*6.0f);
            
        }
    }

    public void GlowCount(int point)
    {
       
    }

    //タグがFoodなら当たったものをCatchにもっていく
    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag != "Untagged")
        {
            collision.transform.GetComponent<Rigidbody>().AddForce(transform.forward * CastAwaySpeed * transform.localScale.z);

            transform.localScale += GrowRate;
        }
        transform.GetComponent<Rigidbody>().AddForce(-transform.forward*2.0f);
        //HP--;        
    }
}
