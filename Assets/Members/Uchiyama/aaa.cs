using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaa : MonoBehaviour {
    public GameObject Efect;
    public GameObject Effect2;
    public GameObject G;
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
        
        gameObject.transform.rotation = new Quaternion(0, 0, 0,0);
        if (flg == true)
        {
            Efect.transform.position =gameObject.transform.position ;
            Instantiate(Efect);
            Effect2.transform.position = gameObject.transform.position;
            Instantiate(Effect2);
            G.transform.position = gameObject.transform.position;
            Instantiate(G);
            Destroy(gameObject);
        }
    }
}
