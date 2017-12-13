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
    private float Interval = 2.0f;

    //時間
   public float SpawnTime;

    //兵士出撃のトリガー
    public bool GoSortie;

    // public int SpawnRandom;

    int iRandNum;

    public static GameObject CreateCastle(string path, Vector3 pos = new Vector3())
    {
        Debug.Log(path + "," + pos.z);
        string temp = path.Substring(0, 7);
        GameObject g;
        try
        {
            g = Instantiate(Resources.Load("Models/" + temp, typeof(GameObject))) as GameObject;
        }
        catch
        {
            //オブジェクトパスが見つからない場合
            g = Instantiate(Resources.Load("Models/OUT_BOX", typeof(GameObject))) as GameObject;
        }
        g.AddComponent<Castle>();
        g.name = temp;
        g.transform.position = pos;

        return g;
    }

    void Initialize()
    {


        //オブジェクトの追加
        ObjectManager.AddObject(gameObject);
    }

    // Use this for initialization
    void Start () {
        Initialize();
        
        SpawnTime = 0.0f;
        GoSortie = false;


        soldier1 = "Prefabs/Enemy_CAL";
        soldier2 = "Prefabs/Enemy_CAL";
    }
   
	
	// Update is called once per frame
	void Update () {

       
       // SpawnRandom = Random.Range(0, 4);

        //兵士沸かせる

        //if (GoSortie == true)
        //{
        //    if (CastleHp > 50)
        //    {
                SpawnTime += Time.deltaTime;

                if (SpawnTime >= Interval)
                {
                    iRandNum=Random.Range(0, 4);
                    Spawn();
                }

        //    }
        //}

        

    }

    //兵士わくわくさん
    void Spawn()
    {


        


        switch (iRandNum)
        {
            case 0:
                DataBaseManager.SpawnEnemyWave("EnemyColumn");
                Debug.Log("縦列");
                break;

            case 1:
                DataBaseManager.SpawnEnemyWave("EnemyUnit");
                Debug.Log("部隊");
                break;
            case 2:
                DataBaseManager.SpawnEnemyWave("EnemyPlatoon");
                Debug.Log("小隊");
                break;
            case 3:
                DataBaseManager.SpawnEnemyWave("EnemyV");
                Debug.Log("V字");
                break;
        }







        if (CastleHp <= 80)
        {

            DataBaseManager.SpawnEnemyWave("EnemyM");

        }

        SpawnTime = 0;
        GoSortie = false;

    }
}
