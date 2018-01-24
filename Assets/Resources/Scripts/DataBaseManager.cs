using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// データベースの読み込みを行うクラス
/// </summary>
public class DataBaseManager : MonoBehaviour {

    public struct OBJECT
    {
        public string type;//タイプ
        public string name;//名前
        public string path;//モデルのパス
        public string cate;//カテゴリ
        public string guid;//オブジェクトの説明
        public float speed;//スピード
        public float scale;//スケール
        public int eat;//食事ポイント
        public Vector3 pos;//生成位置
        public string breedA;//次世代候補A
        public string breedB;//次世代候補B
    }

    static OBJECT[] objectsData;
    public static Player player;
    public static Castle humanCastle;
    public static Castle robotCastle;


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
    public static void SetUpObjectData()
    {
        string temp = Resources.Load("Others/EnemyData", typeof(TextAsset)).ToString();
        int count = 0;
        //行分け
        string[] lineText = temp.Split('\n');
        objectsData = new OBJECT[lineText.Length];
        foreach (string line in lineText)
            if (line.StartsWith("#"))
            {
                //コメントアウトの部分なので何もしない
            }
            else
            {
                //タブ区切り(.TSV) 
                string[] dataText = line.Split('\t');
                objectsData[count].type = dataText[0];
                objectsData[count].name = dataText[1];
                objectsData[count].cate = dataText[2];
                objectsData[count].path = dataText[3];
                objectsData[count].guid = dataText[4];
                if (dataText.Length >= 5) objectsData[count].breedA = dataText[5];
                if (dataText.Length >= 6) objectsData[count].breedA = dataText[6];
                count++;
            }
    }
    //SetUpObjectData()から読み込んだものの中から選択する
    public static string[] SetUpStartObjectData()
    {
        string temp = Resources.Load("Others/GameStartObjectData", typeof(TextAsset)).ToString();
        int count = 0;
        //行分け
        string[] lineText = temp.Split('\n');
        string[] objNames = new string[lineText.Length];
        foreach (string line in lineText)
        {
            char c = line[0];
            switch (c)
            {
                case '#':
                    //コメントアウトの部分なので何もしない
                    break;
                case '@':
                    objNames = new string[int.Parse(line.Substring(1, 3))];
                    break;
                default:
                    objNames[count] = line.Replace('\r','\0');
                    //Debug.Log(line);
                    count++;
                    break;
            }
        }
        //最後の行のみ'\r'が存在しないので1文字減っている
        //'\0'を追加
        objNames[count - 1] += '\0';
        count = 0;
        for(count=0;count<objNames.Length;count++)
            objNames[count]=objNames[count].Substring(0, objNames[count].Length - 1);

        return objNames;
    }

    public static void SetUpStartData()
    {
        string temp = Resources.Load("Others/GameStartData", typeof(TextAsset)).ToString();
        int count = 0;
        //行分け
        string[] lineText = temp.Split('\n');
        string[] objNames = new string[lineText.Length];
        Vector3[] objPos = new Vector3[lineText.Length];
        foreach (string line in lineText)
        {
            string[] dataText = line.Split(' ');
            char c = line[0];
            switch (c)
            {
                case '#':
                    //コメントアウトの部分なので何もしない
                    break;
                default:
                    objNames[count] = dataText[0];
                    objPos[count].x = float.Parse(dataText[1]);
                    objPos[count].y = float.Parse(dataText[2]);
                    objPos[count].z = float.Parse(dataText[3]);
                    count++;
                    break;
            }
        }
        //オブジェクト生成
        player=Player.CreatePlayer(objNames[0],objPos[0]).GetComponent<Player>();
        //ロボット城
        GameObject g1 = RobotCastle.CreateRobotCastle(objNames[1], objPos[1]);
        //人類城
        GameObject g2 = HumanCastle.CreateHumanCastle(objNames[2], objPos[2]);

        g1.transform.LookAt(g2.transform);
        g2.transform.LookAt(g1.transform);

        humanCastle = g1.GetComponent<Castle>();
        robotCastle = g2.GetComponent<Castle>();
    }

    public static OBJECT GetEnemyNum(int num)
    {
        return objectsData[num];
    }
    public static void SpawnEnemy(string path)
    {
        Enemy.CreateEnemy(path);
    }
    public static void SpawnWave(string path,Vector3 offset=new Vector3())
    {
        OBJECT enemy = new OBJECT();

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
                enemy = GetObjectData(dataText[0]);
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
                else
                {
                    enemy.scale = 1.0f;
                }

                enemy.pos += offset;
                CreateObjectType(enemy);
            }
    }
    //検出したタイプによって生成を変更させる
    public static void CreateObjectType(OBJECT data)
    {
        switch (data.type)
        {
            case "A"://危害加えない
                Human.CreateHuman(data);
                break;
            case "B"://敵
                Enemy.CreateEnemy(data);
                break;
            case "C"://静止
                BaseObject.CreateObject(data.path);
                break;
            default:
                break;
        }
    }
    public static OBJECT GetObjectData(string path)
    {
        OBJECT obj = new OBJECT();
        
        foreach (OBJECT data in objectsData)
        {
            if (data.path != null)
                if (data.path== path)
                {
                    Debug.Log(data.path + "," + path);
                    obj = data;
                }
        }
        return obj;
    }
    public static int GetObjectLength()
    {
        return objectsData.Length;
    }
    public static Player GetPlayer()
    {
        return player;
    }
    public static void SetHumanCastle(Castle c)
    {
        humanCastle = c;
    }
    public static void SetRobotCastle(Castle c)
    {
        robotCastle = c;
    }
    public static Vector3 GetHumanCastle()
    {
        if (humanCastle == null) return new Vector3();
        return humanCastle.transform.position;
    }
    public static Vector3 GetRobotCastle()
    {
        if (robotCastle == null) return new Vector3();
        return robotCastle.transform.position;
    }
}
