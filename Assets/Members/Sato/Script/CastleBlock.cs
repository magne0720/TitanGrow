using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBlock : MonoBehaviour {


    GameObject Castleobj;
    public int CastleBlockHp=50;

    //飛んでいく力
    float force =500f;

    //壁破壊のトリガー
    bool wall;



    // Use this for initialization
    void Start () {


        wall = false;

       Castleobj = GameObject.Find("Castle");

    }
	
	// Update is called once per frame
	void Update () {

        //CastleBlockHpが0のとき部位破壊
        if (CastleBlockHp <= 0) {
            wall = true;

            if (wall == true)
            {

                Break();

            }
        }
     
    }

    //城破壊
    public void Break()
    {

            foreach (Transform part in GetComponentInChildren<Transform>())
            {
                ExplodePart(part, force);
            }

    }


    //子を都バス
    public void ExplodePart(Transform part, float force)
    {
        float x = Random.Range(-30.0f, 30.0f);
        float y = Random.Range(20.0f, 10.0f);
        float z = Random.Range(-30.0f, 30.0f);

        part.transform.parent = null;
        part.gameObject.AddComponent<BoxCollider>();
        Rigidbody rb = part.gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
       rb.useGravity = true;
        rb.AddExplosionForce(force, new Vector3(x, y, z), 0.0f);
       // Destroy(part.gameObject, 5f);
        Destroy(this.gameObject,1.5f);
    }
    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "Hand")
        {

            Castle unti = Castleobj.GetComponent<Castle>();
            unti.CastleHp -= 10;
           // unti.GoSortie = true;
            CastleBlockHp -= 10;
            Debug.Log(CastleBlockHp);
 
        }
    }
}
