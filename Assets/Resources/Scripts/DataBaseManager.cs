using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour {

    public struct ENEMY
    {
        public string path;
        public float speed;
        public int eat;
        public Vector3 pos;
    }
    public struct OBJECT
    {
        string path;
        int eat;
    }

    ENEMY[] enemysData;
    OBJECT[] objectsData;

    // Use this for initialization
    void Start () {

        //SetUpEnemyData();
        //SpawnEnemyWave("EnemyData");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnEnemy("Prefabs/test");
        }
	}
    void SetUpDataBase()
    {
        string temp;
        //プレイヤーベース
        temp = Resources.Load("Others/PlayerData", typeof(TextAsset)).ToString();
        //行分け
        string[] lineText = temp.Split('\n');
        foreach (string s in lineText)
            if (s[0] != '#')
            {

            }
    }
    void SetUpEnemyData()
    {
        string temp;
        int count = 0;
        temp = Resources.Load("Others/EnemyData", typeof(TextAsset)).ToString();
        //行分け
        string[] lineText = temp.Split('\n');
        enemysData = new ENEMY[lineText.Length];
        foreach (string s in lineText)
            if (s.StartsWith("#") )
            {
                string[] dataText = s.Split(',');
                enemysData[count].path = dataText[0];
                enemysData[count].speed = float.Parse(dataText[1]);
                enemysData[count].eat = int.Parse(dataText[2]);
                count++;
            }
    }
    public static void SpawnEnemy(string path)
    {
        Enemy.CreateEnemy(path);
    }
    public void SpawnEnemyWave(string path)
    {
        ENEMY enemy = new ENEMY();

        string temp;
        temp = Resources.Load("Others/"+path, typeof(TextAsset)).ToString();
        if(temp==null)
        {
            Debug.Log("NoData[" + temp+"]");
            return;
        }
        //行分け
        string[] lineText = temp.Split('\n');
        enemysData = new ENEMY[lineText.Length];
        foreach (string line in lineText)
            if (line[0] != '#')//コメントアウトは読み込まない
            {
                string[] dataText = line.Split(' ');
                //敵の数
                //敵の種類　ｘ　ｙ　ｚ
                enemy.path = dataText[0];
                enemy.pos.x = int.Parse(dataText[1]);
                enemy.pos.y = int.Parse(dataText[2]);
                enemy.pos.z = int.Parse(dataText[3]);
                Debug.Log(enemy.path + ",{" + enemy.pos.x + "," + enemy.pos.y + "," + enemy.pos.z + "}");
               Enemy.CreateEnemy(enemy);
            }
    }
}
