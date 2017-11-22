using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    //城の体力
    public int CastleHp = 1000;

    //生成する兵士
    public GameObject soldier1;
    public GameObject soldier2;

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
        for (int unit = 0; unit < 3; unit++)
        {
            for (int sol1 = 0; sol1 < soldier1Spawn; sol1++)
            {
                Vector3 pos = new Vector3(2 * sol1 - 4, 0, 2 * unit);
                GameObject.Instantiate(Enemy.Create(soldier1), pos, Quaternion.identity);

            }
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
        GoSortie = false;

    }
}
