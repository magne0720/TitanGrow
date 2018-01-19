using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCastle : Castle {



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
        Initialize(100,50.0f,new Vector3(0,0,150));
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
}
