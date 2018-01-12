using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    //城の体力
    public int CastleHp = 1000;
    
    //一度に生成する量
    private int soldier1Spawn = 5;
    private int soldier2Spawn = 3;

    //生成する間隔
    private float Interval = 30.0f;

    //時間
   public float SpawnTime;

    //兵士出撃のトリガー
    public bool GoSortie;

    // public int SpawnRandom;

    int iRandNum;

    private Vector3 offsetPosition;

    public static GameObject CreateCastle(string path, Vector3 pos = new Vector3())
    {
        Debug.Log(path + "," + pos.z);
        string temp = path.Replace('\r', '\0');
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

        tag = "Castle";
    }

    // Use this for initialization
    void Start () {
        Initialize();

        SpawnTime = Interval;
        GoSortie = false;

        offsetPosition = transform.position + new Vector3(0, 0, -150);

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
                Debug.Log("縦列");
                DataBaseManager.SpawnEnemyWave("EnemyColumn",offsetPosition);
                break;

            case 1:
                Debug.Log("部隊");
                DataBaseManager.SpawnEnemyWave("EnemyUnit", offsetPosition);
                break;
            case 2:
                Debug.Log("小隊");
                DataBaseManager.SpawnEnemyWave("EnemyPlatoon", offsetPosition);
                break;
            case 3:
                Debug.Log("V字");
                DataBaseManager.SpawnEnemyWave("EnemyV", offsetPosition);
                break;
        }
        
        if (CastleHp <= 80)
        {

            Debug.Log("M");
            DataBaseManager.SpawnEnemyWave("EnemyM", offsetPosition);
        }

        SpawnTime = 0;
        GoSortie = false;

    }
}
