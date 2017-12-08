using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

   public void Initialize()
    {
        if (rigid == null)
        {
            rigid = gameObject.AddComponent<Rigidbody>();
            gameObject.AddComponent<CapsuleCollider>().center.Set(0, 0.5f, 0);
            rigid.freezeRotation = true;
        }

        //オブジェクトの追加
        ObjectManager.AddObject(gameObject);

    }
    void OnCollisionEnter(Collision col)
    {

    }

    public static GameObject CreateObject(string path)
    {
        string temp=path.Substring(0, 7);
        GameObject g;
        try
        {
            g = Instantiate(Resources.Load("Models/"+temp, typeof(GameObject))) as GameObject;
        }
        catch
        {
            //オブジェクトパスが見つからない場合
            g = Instantiate(Resources.Load("Models/OUT_BOX", typeof(GameObject))) as GameObject;
            Debug.Log("Object Null="+temp);
        }
        g.AddComponent<BaseObject>();
        g.name = temp;

        return g;
    }
}
