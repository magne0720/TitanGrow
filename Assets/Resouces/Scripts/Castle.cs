using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    //城の体力
    public int CastleHp = 100;

    //生成する兵士
    public GameObject soldier1;
    public GameObject soldier2;

    //一度に生成する量
    private int soldier1Spawn = 5;
    private int soldier2Spawn = 3;
    //生成する間隔
    private float Interval = 3.0f;

    private float Delay = 1.0f;
    public float DelayTime;
    //時間
    private float SpawnTime;
    //飛んでいく力
    private float force = 800f;


    // Use this for initialization
    void Start () {

        
        SpawnTime = 0.0f;
        DelayTime = 0.0f;

    }
	
	// Update is called once per frame
	void Update () {

        //城体力減少　仮設置
        if (Input.GetMouseButtonDown(0))
        {
            CastleHp -= 10;
            Break();
        }


        
        //兵士沸かせる
        if (CastleHp > 50)
        {
            SpawnTime += Time.deltaTime;
           
            if (SpawnTime >= Interval)
            {
                Spawn();
            }
            
        }
        

        
        
		
	}

    void Spawn()
    {
        
        for (int sol1=0; sol1 < soldier1Spawn; sol1++)
        {
            Vector3 pos = new Vector3(2*sol1-4, 0, 0);
            GameObject.Instantiate(Enemy.Create(soldier1), pos, Quaternion.identity);
            
        }

        if (CastleHp <= 80)
        {  
           
                for (int sol2 = 0; sol2 < soldier2Spawn; sol2++)
                {
                    Vector3 pos2 = new Vector3(3 * sol2 - 3, 5, 0);
                    GameObject.Instantiate(soldier2, pos2, Quaternion.identity);
                }
                          
        }
        SpawnTime = 0;
    }



    public void Break()
    {

        if (CastleHp < 60) {
            foreach (Transform part in GetComponentInChildren<Transform>())
            {
                ExplodePart(part, force);
            }
           
        }
    }


    //子を都バス
    public void ExplodePart(Transform part, float force)
    {
        float x = Random.Range(-50.0f, 50.0f);
        float y = Random.Range(-50.0f, -20.0f);
        float z = Random.Range(-50.0f, 50.0f);

        part.transform.parent = null;
        Rigidbody rb = part.gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddExplosionForce(force, new Vector3(x,y, z),0.0f);
        Destroy(part.gameObject, 5f);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Hand")
        {

            CastleHp -= 10;
            Debug.Log(CastleHp);
            Break();
        }
    }
}
