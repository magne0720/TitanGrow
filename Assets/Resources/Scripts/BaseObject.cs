using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour {
    public GameObject Efect;
    public GameObject Effect2;
    public GameObject RockPiece;
    public GameObject WoodPiece;
    public string Type="";
    public bool flg;
    // Use this for initialization
    void Start () {
        flg = false;

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            flg = true;
        }

    }
    void OnCollisionEnter(Collision col)
    {

        //gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        if (flg == true)
        {
            Efect.transform.position = gameObject.transform.position;
            Instantiate(Efect);
            Effect2.transform.position = gameObject.transform.position;
            Instantiate(Effect2);
            GameObject G=null;
            if (Type == "wood")
            {
                G = WoodPiece;
            }
            else if(Type=="rock")
            {
                G = RockPiece;
            }
            G.transform.position = gameObject.transform.position;
            Instantiate(G);
            Destroy(gameObject);
        }
    }

    public static GameObject CreateObject(string path)
    {
        GameObject baseObject;

        if (path == "a")
        {
            baseObject = Instantiate(Resources.Load("Prefabs/test", typeof(GameObject))) as GameObject;
        }
        else
        {
            baseObject = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
        }
        return baseObject;
    }

}
