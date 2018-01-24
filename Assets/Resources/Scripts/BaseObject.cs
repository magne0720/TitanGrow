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
    public override void Initialize()
    {
        ForcePosition = new Vector3();
        MyPosition = transform.position;
        TargetPosition = MyPosition;

        gameObject.AddComponent<Rigidbody>();

        BoxCollider c = gameObject.AddComponent<BoxCollider>();
        c.center = new Vector3(0, 0.5f, 0);


        //オブジェクトの追加
        ObjectManager.AddObject(gameObject);


        transform.tag = "Object";


        foreach (Transform t in this.transform)
        {
            t.gameObject.layer = 8;
        }
        ////透過マテリアルを追加
        //foreach (MeshRenderer g in GetComponentsInChildren<MeshRenderer>())
        //{
        //    g.sharedMaterials = new Material[]
        //    {
        //            g.sharedMaterial,Resources.Load("Shaders/HideOnly")as Material
        //    };
        //}
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
