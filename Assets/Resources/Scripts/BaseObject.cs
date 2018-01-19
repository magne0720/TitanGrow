using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登場するモノのクラス
/// </summary>
public class BaseObject : EatBase
{

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


         public void Move()
    {
        if (transform.parent != null)
        {
            return;
        }
        MyPosition = transform.position;

        if (force > 0) force -= Time.deltaTime * 3.0f;
        else if (force < 0) force = 0;
        Vector3 moving = ForcePosition;
        //moving.Normalize();

        MyPosition += moving * force;

        MyPosition.y = transform.position.y;
        
        if (Math.Length(moving) >= 1.0f)
        {
            Quaternion q = Quaternion.LookRotation(moving);
            transform.rotation = q;
        }
        transform.position = MyPosition;

    }

   public void Initialize()
    {
        gameObject.AddComponent<Rigidbody>();

        BoxCollider c = gameObject.AddComponent<BoxCollider>();
        c.center = new Vector3(0, 0.5f, 0);


        //オブジェクトの追加
        ObjectManager.AddObject(gameObject);

        MyPosition = transform.position;

        transform.tag = "Object";

        try
        {
            foreach (GameObject g in gameObject.GetComponentsInChildren<GameObject>())
            {
                g.gameObject.layer = 8;
            }
        }
        catch { }
        gameObject.layer = 8;
    }
    void OnCollisionEnter(Collision col)
    {

    }

    public static GameObject CreateObject(string path,Vector3 pos=new Vector3())
    {
        //Debug.Log(path+","+pos.z);
        string temp = path.Replace('\r', '\0');
        GameObject g;
        try
        {
            g = Instantiate(Resources.Load("Models/"+temp, typeof(GameObject))) as GameObject;
        }
        catch
        {
            //オブジェクトパスが見つからない場合
            g = Instantiate(Resources.Load("Models/OUT_BOX", typeof(GameObject))) as GameObject;
        }
        g.AddComponent<BaseObject>();
        g.name = temp;
        g.transform.position = pos;

        return g;
    }
}
