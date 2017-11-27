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

        
         for(int count=0;count<3;count++){
            for (int sol1 = 0; sol1 < soldier1Spawn; sol1++)
            {
                Vector3 pos = new Vector3(2 * sol1 - 4, 0, 2*count);
                GameObject g=(Enemy.Create(soldier1));
                g.transform.position = pos;
                
                

            }
            


            }
        


        if (CastleHp <= 80)
        {
            for (int count2 =0; count2 < 2; count2++) {
                for (int sol2 = 0; sol2 < soldier2Spawn; sol2++)
                {
                    Vector3 pos2 = new Vector3(3 * sol2 - 3, 5, 2*count2);
                    GameObject g = (Enemy.Create(soldier2));
                    g.transform.position = pos2;
                }
            }
            
        }

        SpawnTime = 0;
        GoSortie = false;

    }
}
