using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進行
///１．ゲームの立ち上げ
///２．マネージャー
///３．ゲーム設定
///４．オブジェクト情報
///５．オブジェクト追加
///６．ゲーム開始
/// </summary>
public class GameMode : MonoBehaviour {

    public CollisionManager CM;


    // Use this for initialization
    void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartUp()
    {
        //ゲームの立ち上げ(ゲームプレイ時)

        //CollisionManagerの生成
        if (CM == null)
        {
            CM = this.gameObject.AddComponent<CollisionManager>();
        }
        //３．ゲーム設定


        //４．オブジェクト情報
        
        //５．オブジェクト追加
        
        //６．ゲーム開始
    }

    public static void debugPoint(GameObject g)
    {
        for (int x = -2; x <= 2; x++)
            for (int y = -2; y <= 2; y++)
                for (int z = -2; z <= 2; z++)
                {
                    GameObject go = Instantiate(g);
                    go.transform.position = new Vector3(x * 10, z * 10, y * 10);
                }
    }
}
