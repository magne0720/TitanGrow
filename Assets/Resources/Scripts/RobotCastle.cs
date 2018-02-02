using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCastle : Castle {
    
    private int iRandNum;
    private Vector3 playerPosition;


    public static GameObject CreateRobotCastle(string path, Vector3 pos = new Vector3())
    {
        //Debug.Log(path + "," + pos.z);
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
        g.AddComponent<RobotCastle>();
        g.name = temp;
        g.transform.position = pos;

        return g;
    }
    // Use this for initialization
    void Start () {
        Initialize(30,12.0f,20.0f);
        offsetPosition = new Vector3(100,0,0)+transform.position;

	}
	
	// Update is called once per frame
	void Update () {
        SpawnTime += Time.deltaTime;
        if (SpawnTime >= Interval)
        {
            iRandNum = Random.Range(0, 4);
            Spawn();
        }
    }

    public override void Spawn()
    {
        switch (iRandNum)
        {
            case 0:
                //Debug.Log("縦列");
                DataBaseManager.SpawnWave("EnemyColumn", offsetPosition);
                break;
            case 1:
               // Debug.Log("部隊");
                DataBaseManager.SpawnWave("EnemyUnit", offsetPosition);
                break;
            case 2:
                //Debug.Log("小隊");
                DataBaseManager.SpawnWave("EnemyPlatoon", offsetPosition);
                break;
            case 3:
               // Debug.Log("V字");
                DataBaseManager.SpawnWave("EnemyV", offsetPosition);
                break;
        }

        if (CastleHp <= 80)
        {
           // Debug.Log("M");
            DataBaseManager.SpawnWave("EnemyM", offsetPosition);
        }

        SpawnTime = 0;
        GoSortie = false;

    }
    void OnCollisionEnter(Collision c)
    {
        CastleHp--;
    }
}
