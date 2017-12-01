using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    //城の体力
    public int CastleHp = 1000;

    //生成する兵士
    public string soldier1;
    public string soldier2;

    //一度に生成する量
    private int soldier1Spawn = 5;
    private int soldier2Spawn = 3;

    //生成する間隔
    private float Interval = 1.0f;

    //時間
   public float SpawnTime;

    //兵士出撃のトリガー
    public bool GoSortie;



    // Use this for initialization
    void Start () {

        
        SpawnTime = 0.0f;
        GoSortie = false;


        soldier1 = "Prefabs/Enemy_CAL";
        soldier2 = "Prefabs/Enemy_CAL";
    }
   
	
	// Update is called once per frame
	void Update () {

        //兵士沸かせる

        if (GoSortie == true)
        {
            if (CastleHp > 50)
            {
                SpawnTime += Time.deltaTime;

                if (SpawnTime >= Interval)
                {
                    Spawn();
                }

            }
        }

        
		
	}

    //兵士わくわくさん
    void Spawn()
    {

        DataBaseManager.SpawnEnemyWave("EnemyM");



        //if (CastleHp <= 80)
        //{

        //    DataBaseManager.SpawnEnemyWave("EnemyPos");

        //}

        SpawnTime = 0;
        GoSortie = false;

    }
}
