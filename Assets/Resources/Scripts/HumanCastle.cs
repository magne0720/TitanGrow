using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCastle : Castle {

    Vector3[] CreatePositions;


    public static GameObject CreateHumanCastle(string path, Vector3 pos = new Vector3())
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
        g.AddComponent<HumanCastle>();
        g.name = temp;
        g.transform.position = pos;

        return g;
    }
    // Use this for initialization
    void Start () {
        Initialize(1000,20.0f,30);


	}
	
	// Update is called once per frame
	void Update () {
        SpawnTime += Time.deltaTime;
        if (SpawnTime >= Interval)
        {
            Spawn();
            SpawnTime = 0;
        }
	}

    public override void Spawn()
    {
        Human.CreateHuman(DataBaseManager.GetObjectData("hum_001"),offsetPosition);

        SpawnTime = 0;
        GoSortie = false;
    }

    void InitSpawnPosition()
    {
        string temp = Resources.Load("Others/EnemyData", typeof(TextAsset)).ToString();
        int count = 0;
        //行分け
        string[] lineText = temp.Split('\n');
        CreatePositions = new Vector3[lineText.Length];
        foreach (string line in lineText)
            if (line.StartsWith("#"))
            {
                //コメントアウトの部分なので何もしない
            }
            else
            {
                //タブ区切り(.TSV) 
                string[] dataText = line.Split('\t');
                float x = float.Parse(dataText[0]);
                float y = float.Parse(dataText[1]);
                float z = float.Parse(dataText[2]);
                CreatePositions[count] = new Vector3(x, y, z);
                count++;
            }
    }
    void OnCollisionEnter(Collision c)
    {
        CastleHp--;
    }
}
