using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour {

    public struct ENEMY
    {
        public string name;
        public string cate;
        public string guid;
        public string path;
        public Vector3 pos;
        public float speed;
        public float scale;
    }
    public struct OBJECT
    {
        string path;
        int eat;
    }

    static ENEMY[] enemysData;
    static OBJECT[] objectsData;

    // Use this for initialization
    void Start () {
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
        string temp = Resources.Load("Others/PlayerData", typeof(TextAsset)).ToString();
        //行分け
        string[] lineText = temp.Split('\n');
        foreach (string s in lineText)
            if (s[0] != '#')
            {

            }
    }
    public static void SetUpEnemyData()
    {
        string temp = Resources.Load("Others/AboutData", typeof(TextAsset)).ToString();
        int count = 0;
        //行分け
        string[] lineText = temp.Split('\n');
        enemysData = new ENEMY[lineText.Length];
        foreach (string line in lineText)
            if (line.StartsWith("#"))
            {
                //コメントアウトの部分なので何もしない
            }
            else
            {
                //タブ区切り(.TSV) 
                string[] dataText = line.Split('\t');
                enemysData[count].name = dataText[0];
                enemysData[count].cate = dataText[1];
                enemysData[count].guid = dataText[2];
                enemysData[count].path = dataText[3];
                count++;
            }
    }
    public static ENEMY GetEnemyNum(int num)
    {
        return enemysData[num];
    }
    public static void SpawnEnemy(string path)
    {
        Enemy.CreateEnemy(path);
    }
    public static void SpawnEnemyWave(string path,Vector3 offset=new Vector3())
    {
        ENEMY enemy = new ENEMY();

        string temp = Resources.Load("Others/" + path, typeof(TextAsset)).ToString();
        if(temp==null)
        {
            return;
        }
        //行分け
        string[] lineText = temp.Split('\n');
        foreach (string line in lineText)
            if (line[0] != '#')//コメントアウトは読み込まない
            {
                string[] dataText = line.Split(' ');
                //敵の数
                //敵の種類　ｘ　ｙ　Z    scale(記述なしで１)
                enemy = GetEnemyData(dataText[0]);
                enemy.pos.x = float.Parse(dataText[1]);
                enemy.pos.y = float.Parse(dataText[2]);
                enemy.pos.z = float.Parse(dataText[3]);
                if (dataText.Length >= 5)
                {
                    //enemy.scale = float.Parse(dataText[4]);これではいけない
                    //ene_001 2 3 4 ←４の右側に空白がある場合形式エラーが起きる
                    //成功検証のためTryParseを利用
                    if(!float.TryParse(dataText[4],out enemy.scale))
                    {
                        //空白があったら大体スケールの変更はないので等倍
                        enemy.scale = 1.0f;
                    }
                }
               GameObject g=Enemy.CreateEnemy(enemy);
            }
    }
    //パスから敵データの取得
    static ENEMY GetEnemyData(string path)
    {
        ENEMY e = new ENEMY();
        foreach (ENEMY s in enemysData)
        {
            if(s.path!=null)
            if (s.path.Substring(0, 7) == path.Substring(0, 7))
            {
                e = s;
            }
        }
        return e;
    }
}
