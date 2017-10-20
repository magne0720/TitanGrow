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
    private int SpawnAmount = 5;
    //生成する間隔
    private float Interval = 3.0f;
    //時間
    private float SpawnTime;


    // Use this for initialization
    void Start () {

        
        SpawnTime = 0.0f;
		
	}
	
	// Update is called once per frame
	void Update () {
       
            SpawnTime += Time.deltaTime;
            if (SpawnTime >= Interval)
            {
                Spawn();
            }
        
		
	}

    void Spawn()
    {
        for(int i=0; i < SpawnAmount; i++)
        {
            Vector3 pos = new Vector3(2*i, 0, 0);
            GameObject.Instantiate(soldier1, pos, Quaternion.identity);
            if (CastleHp <= 50)
            {
                
                GameObject.Instantiate(soldier2, pos, Quaternion.identity);
            }
        }

        SpawnTime = 0;
    }
}
