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
        //BoxCollider c = gameObject.AddComponent<BoxCollider>();
        //c.center = new Vector3(0, 0.5f, 0);
        //if (rigid == null)
        //{
        //    rigid = gameObject.AddComponent<Rigidbody>();
        //    rigid.freezeRotation = true;
        //    rigid.isKinematic = true;
        //}

        //オブジェクトの追加
        ObjectManager.AddObject(gameObject);

    }
    void OnCollisionEnter(Collision col)
    {

    }

    public static GameObject CreateObject(string path,Vector3 pos=new Vector3())
    {
        Debug.Log(path+","+pos.z);
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
        }
        g.AddComponent<BaseObject>();
        g.name = temp;
        g.transform.position = pos;

        return g;
    }
}
