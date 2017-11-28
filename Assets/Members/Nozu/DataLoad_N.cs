using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoad_N : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ReadFile("EnemyData");
	}
	
	// Update is called once per frame
	void Update () {

    } // 読み込み関数
    void ReadFile(string path)
    {
        string str = Resources.Load("Others/" + path, typeof(TextAsset)) .ToString();
        //行分け
        string[] lineText=str.Split('\n');
        foreach(string s in lineText)
        {
            if (s[0] != '#')
            {
                Debug.Log(s);
            }
        }

    }

}
